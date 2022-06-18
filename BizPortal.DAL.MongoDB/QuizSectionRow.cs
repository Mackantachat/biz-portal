using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BizPortal.DAL.MongoDB
{
    public class QuizSectionRow : Entity
    {
        public static void Init()
        {
            var db = MongoFactory.GetQuizSectionRowCollection();
            if (db.AsQueryable().Count() == 0)
            {
                QuizSectionRow[] items = new QuizSectionRow[]
                {
                    // UTILITIES
                    new QuizSectionRow() {
                        QuizGroup = "UTILITIES",
                        RowNumber = 1,
                        Controls = new List<FormControl> ()
                        {
                            new FormControl()
                            {
                                Control = "HAS_HOME_NUMBER",
                                Type = ControlType.RadioGroup,
                                DataKey = "HAS_HOME_NUMBER",
                                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "HAS_HOME_NUMBER", FixedMessage = true } },
                                RadioGroup = new FormRadioGroup()
                                {
                                    RadioGroupName = "HAS_HOME_NUMBER",
                                    RadioButtons = new FormRadioButton[]
                                    {
                                        new FormRadioButton() { RadioButtonValue = "Yes", RadioButtonText = "HAS_HOME_NUMBER_YES" },
                                        new FormRadioButton() { RadioButtonValue = "No", RadioButtonText = "HAS_HOME_NUMBER_NO" }
                                    }
                                }
                            }
                        }
                    },
                     new QuizSectionRow() {
                        QuizGroup = "UTILITIES",
                        RowNumber = 2,
                        Controls = new List<FormControl> ()
                        {
                            new FormControl()
                            {
                                Control = "METRO_OR_PROVINCE",
                                Type = ControlType.RadioGroup,
                                DataKey = "METRO_OR_PROVINCE",
                                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "METRO_OR_PROVINCE", FixedMessage = true } },
                                RadioGroup = new FormRadioGroup()
                                {
                                    RadioGroupName = "METRO_OR_PROVINCE",
                                    RadioButtons = new FormRadioButton[]
                                    {
                                        new FormRadioButton() { RadioButtonValue = "METRO", RadioButtonText = "METRO" },
                                        new FormRadioButton() { RadioButtonValue = "PROVINCE", RadioButtonText = "PROVINCIAL" }
                                    }
                                }
                            }
                        }
                    },
                     new QuizSectionRow() {
                        QuizGroup = "UTILITIES",
                        RowNumber = 3,
                        Controls = new List<FormControl> ()
                        {
                            new FormControl()
                            {
                                Control = "UTILITY_SERVICES",
                                Type = ControlType.CheckBox,
                                DataKey = "UTILITY_SERVICES",
                                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "UTILITY_SERVICES", FixedMessage = true } },
                                CheckboxName = new string[]
                                {
                                    "ELECTRIC", "WATER", "PHONE"
                                }
                            }
                        }
                    },
                     new QuizSectionRow() {
                        QuizGroup = "UTILITIES",
                        RowNumber = 4,
                        Controls = new List<FormControl> ()
                        {
                            new FormControl()
                            {
                                Control = "INSTALL_TYPE",
                                Type = ControlType.RadioGroup,
                                DataKey = "INSTALL_TYPE",
                                Rules = new FormValidationRule[] { new FormValidationRule() { Type = ValidationType.Required, ErrorMessage = "INSTALL_TYPE", FixedMessage = true } },
                                RadioGroup = new FormRadioGroup()
                                {
                                    RadioGroupName = "INSTALL_TYPE",
                                    RadioButtons = new FormRadioButton[]
                                    {
                                        new FormRadioButton() { RadioButtonValue = "PERMANENT", RadioButtonText = "PERMANENT" },
                                        new FormRadioButton() { RadioButtonValue = "TEMPORARILY", RadioButtonText = "TEMPORARILY" }
                                    }
                                }
                            }
                        }
                    }
                };

                db.InsertMany(items);
            }
        }

        public string QuizGroup { get; set; }

        public int RowNumber { get; set; }

        public IEnumerable<FormControl> Controls { get; set; }

        public static QuizSectionRow[] GetQuizSectionRows(string groupName)
        {
            var db = MongoFactory.GetQuizSectionRowCollection().AsQueryable();
            var rows = db.Where(o => o.QuizGroup == groupName.ToUpper()).ToArray();
            return rows;
        }
    }
}
