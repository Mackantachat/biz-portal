using BizPortal.DAL.MongoDB;
using BizPortal.Models;
using BizPortal.ViewModels;
using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizPortal.Service
{
    public class ChatService
    {
        public static ApplicationRequestEntity InsertChat(ApplicationRequestEntity request, ApplicationRequestViewModel model, ApplicationUser user, ref ResponseData<object> response)
        {
            if (user != null) //found
            {
                if (isRequestFromChat(model)) //found chat from view
                {
                    string chatText = model.Chats.FirstOrDefault().ChatText;
                    if (!string.IsNullOrEmpty(chatText))
                    {
                        if (request.Chats == null)
                        {
                            request.Chats = new List<ApplicationRequestChatEntity>();
                        }

                        request.Chats.Add(new ApplicationRequestChatEntity()
                        {
                            ChatText = chatText,
                            ChatUserID = user.Id,
                            ChatUserFullName = user.FullName,
                            ChatUserType = user.UserType
                        });
                        request.Update();
                        response.Type = ResultDataType.Success;
                        response.Message = Resources.ApplicationStatusRequests.MSG_CHAT_SUCCESS;
                    }
                }
            }
            return request;
        }

        public static bool isRequestFromChat(ApplicationRequestViewModel model)
        {
            bool isChat = false;
            if (model.Chats != null
                    && model.Chats.Length > 0) //found chat from view
            {
                isChat = true;
            }
            return isChat;
        }

        public static string CalculateTimeAgo(DateTime yourDateTime)
        {

            DateTime chatTime = yourDateTime;

            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(chatTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("about {0} minutes ago", timeSpan.Minutes) :
                    "about a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("about {0} hours ago", timeSpan.Hours) :
                    "about an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("about {0} days ago", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("about {0} months ago", timeSpan.Days / 30) :
                    "about a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("about {0} years ago", timeSpan.Days / 365) :
                    "about a year ago";
            }
            return result;
        }
    }
}