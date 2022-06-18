using Mapster;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.DAL.MongoDB
{
    public class SingleFormTransaction : Entity
    {
        public const int STEP_FORM = 0;
        public const int STEP_DOC = 1;
        public const int STEP_CONFIRM = 2;

        public SingleFormTransaction()
        {
            //TransactionID = Guid.NewGuid();
            Apps = new List<string>();
            Files = new List<string>();
            UploadedFiles = new List<FileGroupEntity>();
        }

        public string IdentityID { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? TransactionID { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public int Step { get; set; }

        public int AppStep { get; set; }
        public string BusinessId { get; set; }

        public int AppStepTotal { get; set; }

        public int FileCnt { get; set; }

        public int FileTotal { get; set; }

        public List<string> Apps { get; set; }

        public List<string> Files { get; set; }

        public List<FileGroupEntity> UploadedFiles { get; set; }

        public DateTime? ConsentTimeStamp { get; set; }

        public static SingleFormTransaction Get(string IdentityID)
        {
            var db = MongoFactory.GetSingleFormTransactionCollection().AsQueryable();
            var tran = db.Where(o => o.IdentityID == IdentityID).SingleOrDefault();

            return tran;
        }

        public static SingleFormTransaction Get(Guid TransactionID)
        {
            var db = MongoFactory.GetSingleFormTransactionCollection().AsQueryable();
            var tran = db.Where(o => o.TransactionID == TransactionID).SingleOrDefault();

            return tran;
        }

        public void Save()
        {
            var db = MongoFactory.GetSingleFormTransactionCollection();
            var tran = db.AsQueryable().Where(o => o.IdentityID == IdentityID && o.TransactionID == this.TransactionID).Single();

            if (UploadedFiles != null && UploadedFiles.Count > 0)
            {
                var groupDesc = new List<string>();
                // Check Uploaded Files in transaction
                foreach (var group in UploadedFiles)
                {
                    groupDesc.Add(group.Description);

                    var oldGroup = tran.UploadedFiles.Where(o => o.FileGroupID == group.FileGroupID).SingleOrDefault();
                    if (oldGroup != null)
                    {
                        if (oldGroup.Description == "FREE_DOC_SECTION")
                        {
                            oldGroup.Files.Clear();
                        }
                        foreach (var file in group.Files)
                        {
                            var oldFile = oldGroup.Files.Where(o => o.FileTypeCode == file.FileTypeCode).SingleOrDefault();
                            if (oldFile != null)
                            {
                                if (oldFile.FileID != file.FileID)
                                {
                                    oldGroup.Files.Remove(oldFile);
                                    oldGroup.Files.Add(file);
                                }
                                else
                                {
                                    oldFile.Extras = file.Extras;
                                    oldFile.FileReason = file.FileReason;
                                }
                            }
                            else
                            {
                                oldGroup.Files.Add(file);
                            }
                        }

                        //var existingFileIDs = oldGroup.Files.Select(o => o.FileID);
                        //var newFiles = group.Files.Where(o => !existingFileIDs.Contains(o.FileID));
                        //oldGroup.Files.AddRange(newFiles);
                        //TypeAdapter.Adapt(oldGroup, group);
                    }
                    else
                    {
                        tran.UploadedFiles.Add(group);
                    }
                }

                var disGroup = tran.UploadedFiles.Where(o => !groupDesc.Contains(o.Description)).ToList();
                foreach (var group in disGroup)
                {
                    tran.UploadedFiles.Remove(group);
                }
                var curGroups = tran.UploadedFiles.ToList();
                foreach (var group in tran.UploadedFiles)
                {
                    var newGroup = this.UploadedFiles.Where(o => o.Description == group.Description).Single();
                    group.Files.RemoveAll(x => newGroup.Files.Where(f => f.FileName == x.FileName).Count() == 0);
                }

                // Save Uploaded Files to MongoDB
                var fgdb = MongoFactory.GetFileGroupCollection();
                var fdb = MongoFactory.GetFileMetadataCollection();

                foreach (var group in tran.UploadedFiles)
                {
                    foreach (var file in group.Files)
                    {
                        if (string.IsNullOrEmpty(file.Id))
                        {
                            file.TransactionID = (Guid)tran.TransactionID;
                            fdb.InsertOne(file);
                        }
                        else
                        {
                            file.UpdatedDate = DateTime.Now;
                            fdb.Update(file);
                        }
                    }

                    if (string.IsNullOrEmpty(group.Id))
                    {
                        if (group.Files.Count > 0)
                        {
                            group.TransactionID = (Guid)tran.TransactionID;
                            fgdb.InsertOne(group);
                        }
                    }
                    else
                    {
                        if (group.Files.Count > 0)
                        {
                            group.UpdatedDate = DateTime.Now;
                            fgdb.Update(group);
                        }
                        else
                        {
                            fgdb.Delete(group);
                        }
                    }
                }
            }
            else
            {
                tran.UploadedFiles.Clear();
            }
            tran.FileCnt = this.FileCnt;
            tran.FileTotal = this.FileTotal;
            db.Update(tran);
        }

        public void RemoveUploadedFiles(Guid fileGroupID, string[] fileIDs)
        {
            var db = MongoFactory.GetFileGroupCollection();
            var group = UploadedFiles.Where(o => o.FileGroupID == fileGroupID).SingleOrDefault();
            if (group != null)
            {
                group.Files = group.Files.Where(o => !fileIDs.Contains(o.FileID)).ToList();

                if (group.Files.Count == 0)
                {
                    db.Delete(group);
                }
                else
                {
                    db.Update(group);
                }
            }
            UploadedFiles = GetUploadedFiles();
        }

        public void RemoveUploadedFiles(Guid fileGroupID)
        {
            var db = MongoFactory.GetSingleFormTransactionCollection();
            var group = UploadedFiles.Where(o => o.FileGroupID == fileGroupID).SingleOrDefault();
            if (group != null)
            {
                UploadedFiles.Remove(group);
                db.Update(this);
            }
        }

        private List<FileGroupEntity> GetUploadedFiles()
        {
            var db = MongoFactory.GetFileGroupCollection().AsQueryable();
            return db.Where(o => o.TransactionID == TransactionID).ToList();
        }
    }
}
