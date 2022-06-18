using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using BizPortal.ViewModels.Report;


namespace BizPortal.DAL.MongoDB
{
	public class SmartQuiz : Entity
	{
		public static void Init()
		{
			string BUSINESS_RESTAURANT_TYPE = BusinessGroupID.RESTAURANT;
			var db = MongoFactory.GetSmartQuizCollection();
			if (db.AsQueryable().Count() == 0)
			{
				var quizRestuarant = new SmartQuiz(BUSINESS_RESTAURANT_TYPE)
				{
					Text = "QUIZ_OPEN_RESTUARANT"
				};
				var qAlcohol = new Question
				{
					SystemName = "QUESTION_RESTUARANT_SELL_ALCOHOL",
					Text = "QUESTION_RESTUARANT_SELL_ALCOHOL",
					Icon = "~/img/SmartQuiz/7 wine.svg",
				};
				var qAlcohol_yes = new Choice
				{
					SystemName = "YES",
					Text = "QUESTION_RESTUARANT_SELL_ALCOHOL__CHOICE_YES"
				};
				var qAlcohol_no = new Choice
				{
					SystemName = "No",
					Text = "QUESTION_RESTUARANT_SELL_ALCOHOL__CHOICE_NO"
				};
				qAlcohol.Choices.Add(qAlcohol_yes);
				qAlcohol.Choices.Add(qAlcohol_no);
				//qAlcohol.SelectedChoices.Add(qAlcohol_yes.ChoiceID);
				quizRestuarant.Questions.Add(qAlcohol);

				var qTobacco = new Question
				{
					SystemName = "QUESTION_RESTUARANT_SELL_TOBACCO",
					Text = "QUESTION_RESTUARANT_SELL_TOBACCO",
					Icon = "~/img/SmartQuiz/9 cigar.svg",
				};
				var qTobacco_yes = new Choice
				{
					SystemName = "YES",
					Text = "QUESTION_RESTUARANT_SELL_TOBACCO__CHOICE_YES"
				};
				var qTobacco_no = new Choice
				{
					SystemName = "No",
					Text = "QUESTION_RESTUARANT_SELL_TOBACCO__CHOICE_NO"
				};
				qTobacco.Choices.Add(qTobacco_yes);
				qTobacco.Choices.Add(qTobacco_no);
				quizRestuarant.Questions.Add(qTobacco);
				qAlcohol.NextQuestionID = qTobacco.QuestionID;

				var qStoreGas = new Question
				{
					SystemName = "QUESTION_RESTUARANT_STORE_GAS",
					Text = "QUESTION_RESTUARANT_STORE_GAS",
					Icon = "~/img/SmartQuiz/10 gas.svg",
				};
				var qStoreGas_no = new Choice
				{
					SystemName = "NO",
					Text = "QUESTION_RESTUARANT_STORE_GAS__CHOICE_NO"
				};
				var qStoreGas_le1000kg = new Choice
				{
					SystemName = "LE1000KG",
					Text = "QUESTION_RESTUARANT_STORE_GAS__CHOICE_LE1000KG"
				};
				var qStoreGas_gt1000kg = new Choice
				{
					SystemName = "GT1000KG",
					Text = "QUESTION_RESTUARANT_STORE_GAS__CHOICE_GT1000KG"
				};

				qStoreGas.Choices.Add(qStoreGas_no);
				qStoreGas.Choices.Add(qStoreGas_le1000kg);
				qStoreGas.Choices.Add(qStoreGas_gt1000kg);
				quizRestuarant.Questions.Add(qStoreGas);
				qTobacco.NextQuestionID = qStoreGas.QuestionID;

				var questionService = new Question
				{
					SystemName = "QUESTION_RESTUARANT_SERVICE",
					Text = "QUESTION_RESTUARANT_SERVICE",
					AllowMultipleAnswer = true,
					Icon = "~/img/SmartQuiz/8 dance.svg",
				};
				var questionService_livePerformanceBeforeMidnight = new Choice
				{
					SystemName = "LIVE_PERFORM_BEFORE_MIDNIGHT",
					Text = "QUESTION_RESTUARANT_SERVICE__LIVE_PERFORM_BEFORE_MIDNIGHT"
				};
				var questionService_livePerformanceAfterMidnight = new Choice
				{
					SystemName = "LIVE_PERFORM_AFTER_MIDNIGHT",
					Text = "QUESTION_RESTUARANT_SERVICE__LIVE_PERFORM_AFTER_MIDNIGHT"
				};
				var questionService_karaoke = new Choice
				{
					SystemName = "KARAOKE",
					Text = "QUESTION_RESTUARANT_SERVICE__KARAOKE"
				};
				var questionService_dance = new Choice
				{
					SystemName = "DANCE",
					Text = "QUESTION_RESTUARANT_SERVICE__DANCE"
				};
				var questionService_danceFloor = new Choice
				{
					SystemName = "DANCE_FLOOR",
					Text = "QUESTION_RESTUARANT_SERVICE__DANCE_FLOOR"
				};
				var questionService_drinkPartner = new Choice
				{
					SystemName = "DRINK_PARTNER",
					Text = "QUESTION_RESTUARANT_SERVICE__DRINK_PARTNER"
				};
				var questionService_no = new Choice
				{
					SystemName = "NO",
					Text = "QUESTION_RESTUARANT_SERVICE__NO",
					//ForbiddenInfo = "QUESTION_RETAIL_PRODUCT__MEDICINE_FORBIDDEN_INFO",
					ForbiddenChoices = new List<Guid>
					{
						questionService_livePerformanceBeforeMidnight.ChoiceID,
						questionService_livePerformanceAfterMidnight.ChoiceID,
						questionService_karaoke.ChoiceID,
						questionService_dance.ChoiceID,
						questionService_danceFloor.ChoiceID,
						questionService_drinkPartner.ChoiceID,
					}
				};
				qStoreGas.NextQuestionID = questionService.QuestionID;
				questionService.Choices.Add(questionService_livePerformanceBeforeMidnight);
				questionService.Choices.Add(questionService_livePerformanceAfterMidnight);
				questionService.Choices.Add(questionService_karaoke);
				questionService.Choices.Add(questionService_dance);
				questionService.Choices.Add(questionService_danceFloor);
				questionService.Choices.Add(questionService_drinkPartner);
				questionService.Choices.Add(questionService_no);
				//questionService.SelectedChoices.Add(questionService_livePerformanceBeforeMidnight.ChoiceID);
				//questionService.SelectedChoices.Add(questionService_drinkPartner.ChoiceID);
				quizRestuarant.Questions.Add(questionService);
				var qFootpath = new Question
				{
					SystemName = "QUESTION_RESTUARANT_FOOTPATH",
					Text = "QUESTION_RESTUARANT_FOOTPATH",
					Info = "QUESTION_RESTUARANT_FOOTPATH__INFO",
					Icon = "~/img/SmartQuiz/5 street.svg",
				};
				questionService.NextQuestionID = qFootpath.QuestionID;
				var qFootpath_yes = new Choice
				{
					SystemName = "YES",
					Text = "QUESTION_RESTUARANT_FOOTPATH__YES"
				};
				var qFootpath_no = new Choice
				{
					SystemName = "NO",
					Text = "QUESTION_RESTUARANT_FOOTPATH__NO"
				};
				qFootpath.Choices.Add(qFootpath_yes);
				qFootpath.Choices.Add(qFootpath_no);
				quizRestuarant.Questions.Add(qFootpath);

				var qArea = new Question
				{
					SystemName = "QUESTION_RESTUARANT_AREA",
					Text = "QUESTION_RESTUARANT_AREA",
					Icon = "~/img/SmartQuiz/6 noodles.svg",
				};
				qFootpath.NextQuestionID = qArea.QuestionID;
				var qArea_lt200 = new Choice
				{
					SystemName = "LT200SQM",
					Text = "QUESTION_RESTUARANT_AREA__LT200SQM"
				};
				var qArea_ge200 = new Choice
				{
					SystemName = "GE200SQM",
					Text = "QUESTION_RESTUARANT_AREA__GE200SQM"
				};
				qArea.Choices.Add(qArea_lt200);
				qArea.Choices.Add(qArea_ge200);
				quizRestuarant.Questions.Add(qArea);

				var qDanger = new Question
				{
					SystemName = "QUESTION_RESTUARANT_DANGER",
					Text = "QUESTION_RESTUARANT_DANGER",
					Icon = "~/img/SmartQuiz/9 oven.svg",
				};
				qArea.NextQuestionID = qDanger.QuestionID;
				var qDanger_yes = new Choice
				{
					SystemName = "YES",
					Text = "QUESTION_RESTUARANT_DANGER__YES"
				};
				var qDanger_no = new Choice
				{
					SystemName = "NO",
					Text = "QUESTION_RESTUARANT_DANGER__NO"
				};
				qDanger.Choices.Add(qDanger_yes);
				qDanger.Choices.Add(qDanger_no);
				//quizRestuarant.Questions.Add(qDanger);

				List<Choice> dangerFoodChoices = new List<Choice>();
				for (int i = 1; i < 27; i++)
				{
					var c = new Choice
					{
						SystemName = "QUESTION_RESTUARANT_DANGER_ITEM_C" + i,
						Text = "QUESTION_RESTUARANT_DANGER_ITEM_C" + i,
					};
					dangerFoodChoices.Add(c);
				}
				var dangerNone = new Choice
				{
					SystemName = "QUESTION_RESTUARANT_DANGER_ITEM_None",
					Text = "QUESTION_RESTUARANT_DANGER_ITEM_None",
					ForbiddenChoices = dangerFoodChoices.Select(x => x.ChoiceID).ToList(),
				};
				dangerFoodChoices.Add(dangerNone);

				var qDangerList = new Question
				{
					SystemName = "QUESTION_RESTUARANT_DANGER_ITEM",
					Text = "QUESTION_RESTUARANT_DANGER_ITEM",
					AllowMultipleAnswer = true,
					Choices = dangerFoodChoices
				};

				//List<Choice> dangerConstChoices = new List<Choice>();
				//for (int i = 1; i < 27; i++)
				//{
				//    var c = new Choice
				//    {
				//        SystemName = "QUESTION_RESTUARANT_DANGER_CONST_C" + i,
				//        Text = "QUESTION_RESTUARANT_DANGER_CONST_C" + i,
				//    };
				//    dangerConstChoices.Add(c);
				//}
				//var dangerConstNone = new Choice
				//{
				//    SystemName = "QUESTION_RESTUARANT_DANGER_CONST_None",
				//    Text = "QUESTION_RESTUARANT_DANGER_CONST_None",
				//    ForbiddenChoices = dangerConstChoices.Select(x => x.ChoiceID).ToList(),
				//};
				//dangerConstChoices.Add(dangerConstNone);

				//var qDangerConstList = new Question
				//{
				//    SystemName = "QUESTION_RESTUARANT_DANGER_CONST_ITEM",
				//    Text = "QUESTION_RESTUARANT_DANGER_CONST_ITEM",
				//    AllowMultipleAnswer = true,
				//    Choices = dangerConstChoices
				//};

				qDanger.SubQuestions.Add(qDangerList);
				qDanger_yes.NextQuestionID = qDangerList.QuestionID.ToString();
				quizRestuarant.Questions.Add(qDanger);

				// Append sign-tax questions
				AddSignTaxQuestion(quizRestuarant, BUSINESS_RESTAURANT_TYPE);

				// Add construction question
				AddShopConstructionQuestion(quizRestuarant, BUSINESS_RESTAURANT_TYPE);

				db.InsertOne(quizRestuarant);

				// ===========================================================
				// Quiz App Mapping Init
				// ===========================================================
				var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
				QuizAppMapping[] items = new QuizAppMapping[]
				{
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = AppSystemNameTextConst.APP_SELL_ALCOHOL,
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qAlcohol.SystemName, qAlcohol_yes.SystemName)
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = AppSystemNameTextConst.APP_SELL_TOBACCO,
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qTobacco.SystemName, qTobacco_yes.SystemName)
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = AppSystemNameTextConst.APP_STORE_GAS_LE1000KG,
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qStoreGas.SystemName, qStoreGas_le1000kg.SystemName)
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = AppSystemNameTextConst.APP_STORE_GAS_GT1000KG,
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qStoreGas.SystemName, qStoreGas_gt1000kg.SystemName)
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = "ขอใบอนุญาตตั้งสถานบริการในท้องที่กรุงเทพมหานคร",
						DisplayConditions = new QuizAnswer[] {
							new QuizAnswerGroup(true, new QuizAnswer[]
							{
								new QuizAnswer(questionService.SystemName + "_" + questionService_livePerformanceAfterMidnight.SystemName, "on"),
								new QuizAnswer(questionService.SystemName + "_" + questionService_dance.SystemName, "on"),
								new QuizAnswer(questionService.SystemName + "_" + questionService_danceFloor.SystemName, "on"),
								new QuizAnswer(questionService.SystemName + "_" + questionService_drinkPartner.SystemName, "on"),
							})
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = "ขอใบอนุญาตจำหน่ายสินค้าในที่หรือทางสาธารณะ",
						DisplayConditions = new QuizAnswer[] {
							new QuizAnswer(qFootpath.SystemName, qFootpath_yes.SystemName)
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200,
						DisplayConditions = new QuizAnswer[] {
							new QuizAnswer(qArea.SystemName, qArea_lt200.SystemName)
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200,
						DisplayConditions = new QuizAnswer[] {
							new QuizAnswer(qArea.SystemName, qArea_ge200.SystemName)
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},

					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ",
						DisplayConditions = new QuizAnswer[] {
							new QuizAnswerGroup(true, new QuizAnswer[]
							{
								new QuizAnswer(questionService.SystemName + "_" + questionService_livePerformanceAfterMidnight.SystemName, "on"),
								new QuizAnswer(questionService.SystemName + "_" + questionService_dance.SystemName, "on"),
								new QuizAnswer(questionService.SystemName + "_" + questionService_karaoke.SystemName, "on"),
								new QuizAnswer(questionService.SystemName + "_" + questionService_danceFloor.SystemName, "on"),
								new QuizAnswer(questionService.SystemName + "_" + questionService_livePerformanceBeforeMidnight.SystemName, "on"),
							})
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = "ขอใบอนุญาตประกอบกิจการร้านวีดิทัศน์ (คาราโอเกะ)",
						DisplayConditions = new QuizAnswer[] {
							new QuizAnswer(questionService.SystemName + "_" + questionService_karaoke.SystemName, "on")
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_RESTAURANT_TYPE,
						AppSystemName = "จดทะเบียนพาณิชย์",
						DisplayConditions = new QuizAnswer[] {
							new QuizAnswerGroup(true, new QuizAnswer[]
							{
								new QuizAnswer(QuizAnswer.UserIdentityType, UserTypeEnum.Citizen.ToString()),
								new QuizAnswer(QuizAnswer.UserIdentityType, UserTypeEnum.Anonymous.ToString())
							})
						},
						OnlineRequestAllowedConditions = new QuizAnswer[0],
						Warnings = new AppWarning[]
						{
							new AppWarning
							{
								Message = "APP_WARNING_JURISTIC_NO_NEED_TRADER_REGISTRATION",
								Conditions = new QuizAnswer[]
								{
									new QuizAnswer(QuizAnswer.UserIdentityType, UserTypeEnum.Anonymous.ToString())
								}
							}
						}
					},
				};
				List<QuizAppMapping> dangerFoodQAM = new List<QuizAppMapping>();
				foreach (var c in dangerFoodChoices)
				{
					if (c != dangerNone)
					{
						var qam = new QuizAppMapping()
						{
							QuizGroup = BUSINESS_RESTAURANT_TYPE,
							AppSystemName = AppSystemNameTextConst.APP_DANGER_PREFIX + BizPortal.Utils.Helpers.ResourceHelper.GetResourceWordWithDefault(c.SystemName, "Apps_SmartQuiz"),
							DisplayConditions = new QuizAnswer[] {
								new QuizAnswer(qDanger.SystemName, qDanger_yes.SystemName),
								QuizAnswerFactory.AnswerChecked(qDangerList, c),
							},
							OnlineRequestAllowedConditions = new QuizAnswer[] { },
						};
						dangerFoodQAM.Add(qam);
					}
				}

				// Append sign-tax app mappings
				items = AddSignTaxAppMappings(items, BUSINESS_RESTAURANT_TYPE);

				// Add construction app mapping
				items = AddShopConstructionAppMappings(items, BUSINESS_RESTAURANT_TYPE);

				dbAppMapping.InsertMany(items);
				dbAppMapping.InsertMany(dangerFoodQAM);
				InitRetailQuiz();
				InitUtilitiesQuiz();
				InitStartingBusinessQuiz();
				InitAnimalQuiz();
				InitConstructionQuiz();
				InitElectronicQuiz();
				InitCarCareQuiz();
				InitCoffeeShopQuiz();
				InitFitnessQuiz();
				InitHotelQuiz();
				InitSpaQuiz();
				InitTourismQuiz();
				InitLegalQuiz();
				InitSoftwareQuiz();
				InitLogisticsQuiz();
				InitEducationQuiz();
				InitElderlyCareQuiz();
				InitMedicalToolsQuiz();
				InitBeautyClinicQuiz();
				InitEnergyQuiz();
				InitOrganicFarmingQuiz();
				InitCosmeticsQuiz();
				InitSmallAgriculturalQuiz();
				InitEcomClothesQuiz();
				InitOnlineCosmeticQuiz();
				InitFinanceQuiz();

			}
		}

		private static void InitRetailQuiz()
		{
			string BUSINESS_RETAIL_TYPE = BusinessGroupID.RETAIL;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizRetail = new SmartQuiz(BUSINESS_RETAIL_TYPE)
			{
				Text = "QUIZ_OPEN_RETAIL"
			};
			var qProduct = new Question
			{
				SystemName = "QUESTION_RETAIL_PRODUCT",
				Text = "QUESTION_RETAIL_PRODUCT",
				Icon = "~/img/SmartQuiz/4 shop.svg",
				AllowMultipleAnswer = true
			};

			var qProduct_alcohol = new Choice
			{
				SystemName = "ALCOHOL",
				Text = "QUESTION_RETAIL_PRODUCT__ALCOHOL"
			};
			var qProduct_DVD = new Choice
			{
				SystemName = "DVD",
				Text = "QUESTION_RETAIL_PRODUCT__DVD"
			};
			var qProduct_Karaoke = new Choice
			{
				SystemName = "KARAOKE",
				Text = "QUESTION_RETAIL_PRODUCT__KARAOKE"
			};
			var qProduct_tobacco = new Choice
			{
				SystemName = "TOBACCO",
				Text = "QUESTION_RETAIL_PRODUCT__TOBACCO"
			};
			var qProduct_card = new Choice
			{
				SystemName = "CARD",
				Text = "QUESTION_RETAIL_PRODUCT__CARD"
			};
			var qProduct_insecticide = new Choice
			{
				SystemName = "INSECTICIDE",
				Text = "QUESTION_RETAIL_PRODUCT__INSECTICIDE"
			};
			var qProduct_animal = new Choice
			{
				SystemName = "ANIMAL",
				Text = "QUESTION_RETAIL_PRODUCT__ANIMAL",
				Info = "QUESTION_RETAIL_PRODUCT__ANIMAL_INFO",
			};
			var qProduct_carcasses1 = new Choice
			{
				SystemName = "CARCASSES",
				Text = "QUESTION_RETAIL_PRODUCT__CARCASSES",
				Info = "QUESTION_RETAIL_PRODUCT__CARCASSES_INFO"
			};
			var qProduct_carcasses2 = new Choice
			{
				SystemName = "CARCASSES2",
				Text = "QUESTION_RETAIL_PRODUCT__CARCASSES2",
			};
			var qProduct_carcasses3 = new Choice
			{
				SystemName = "CARCASSES3",
				Text = "QUESTION_RETAIL_PRODUCT__CARCASSES3",
			};
			var qProduct_seed = new Choice
			{
				SystemName = "SEED",
				Text = "QUESTION_RETAIL_PRODUCT__SEED",
				Info = "QUESTION_RETAIL_PRODUCT__SEED_INFO"
			};
			var qProduct_animalFood = new Choice
			{
				SystemName = "ANIMAL_FOOD",
				Text = "QUESTION_RETAIL_PRODUCT__ANIMAL_FOOD",
				Info = "QUESTION_RETAIL_PRODUCT__ANIMAL_FOOD_INFO"
			};
			var qProduct_fertilizer = new Choice
			{
				SystemName = "FERTILIZER",
				Text = "QUESTION_RETAIL_PRODUCT__FERTILIZER"
			};
			var qProduct_medicine = new Choice
			{
				SystemName = "MEDICINE",
				Text = "QUESTION_RETAIL_PRODUCT__MEDICINE",
				Info = "QUESTION_RETAIL_PRODUCT__MEDICINE_INFO",
				ForbiddenInfo = "QUESTION_RETAIL_PRODUCT__MEDICINE_FORBIDDEN_INFO",
				ForbiddenChoices = new List<Guid>
				{
					qProduct_alcohol.ChoiceID,
					qProduct_tobacco.ChoiceID,
					qProduct_animal.ChoiceID,
					qProduct_carcasses1.ChoiceID,
					qProduct_fertilizer.ChoiceID
				}
			};
			qProduct.Choices.Add(qProduct_alcohol);
			qProduct.Choices.Add(qProduct_DVD);
			qProduct.Choices.Add(qProduct_Karaoke);
			qProduct.Choices.Add(qProduct_tobacco);
			qProduct.Choices.Add(qProduct_card);
			qProduct.Choices.Add(qProduct_insecticide);
			qProduct.Choices.Add(qProduct_animal);
			qProduct.Choices.Add(qProduct_carcasses1);
			qProduct.Choices.Add(qProduct_carcasses2);
			qProduct.Choices.Add(qProduct_carcasses3);
			qProduct.Choices.Add(qProduct_seed);
			qProduct.Choices.Add(qProduct_animalFood);
			qProduct.Choices.Add(qProduct_fertilizer);
			qProduct.Choices.Add(qProduct_medicine);
			quizRetail.Questions.Add(qProduct);

			var qCosmetic = new Question
			{
				SystemName = "QUESTION_RETAIL_COSMETIC_PRODUCER",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER",
				Icon = "~/img/SmartQuiz/10 lipstick.svg",
				Info = "QUESTION_RETAIL_COSMETIC_PRODUCER_INFO"
			};
			var qCosmetic_yes = new Choice
			{
				SystemName = "QUESTION_RETAIL_COSMETIC_PRODUCER__YES",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER__YES",
			};
			var qCosmetic_no = new Choice
			{
				SystemName = "QUESTION_RETAIL_COSMETIC_PRODUCER__NO",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER__NO",
			};
			qCosmetic.Choices.AddRange(new Choice[]
			{
				qCosmetic_yes,
				qCosmetic_no,
			});
			var qCosmeticPermit = new Question
			{
				SystemName = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT",
			};
			var qCosmeticPermit_yes = new Choice
			{
				SystemName = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT__YES",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT__YES",
			};
			var qCosmeticPermit_no = new Choice
			{
				SystemName = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT__NO",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT__NO",
			};
			qCosmeticPermit.Choices.AddRange(new Choice[]
			{
				qCosmeticPermit_yes,
				qCosmeticPermit_no,
			});
			qCosmetic.SubQuestions.Add(qCosmeticPermit);
			qCosmetic_yes.NextQuestionID = qCosmeticPermit.QuestionID.ToString();
			quizRetail.Questions.Add(qCosmetic);

			var qSellFood = new Question
			{
				SystemName = "QUESTION_RETAIL_SELL_FOOD",
				Text = "QUESTION_RETAIL_SELL_FOOD",
				Icon = "~/img/SmartQuiz/7 food.svg",
			};
			var qSellFood_yes = new Choice
			{
				SystemName = "YES",
				Text = "QUESTION_RETAIL_SELL_FOOD__YES",
			};
			var qSellFood_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_RETAIL_SELL_FOOD__NO",
			};
			qSellFood.Choices.AddRange(new Choice[]
			{
				qSellFood_yes,
				qSellFood_no
			});
			quizRetail.Questions.Add(qSellFood);

			var qArea = new Question
			{
				SystemName = "QUESTION_RETAIL_AREA",
				Text = "QUESTION_RETAIL_AREA"
			};
			var qArea_small = new Choice
			{
				SystemName = "LT200SQM",
				Text = "QUESTION_RETAIL_AREA__LT200SQM"
			};
			var qArea_large = new Choice
			{
				SystemName = "GE200SQM",
				Text = "QUESTION_RETAIL_AREA__GE200SQM"
			};
			qArea.Choices.AddRange(new Choice[]
			{
				qArea_small,
				qArea_large
			});
			qSellFood_yes.NextQuestionID = qArea.QuestionID.ToString();
			qSellFood.SubQuestions = new List<Question> { qArea };

			var qShopOwner = new Question
			{
				SystemName = "QUESTION_RETAIL_SHOP_OWNER",
				Text = "QUESTION_RETAIL_SHOP_OWNER",
				Icon = "~/img/SmartQuiz/3 money.svg",
			};

			var qShopOwner_CitizenBelow18m = new Choice
			{
				SystemName = "QUESTION_RETAIL_SHOP_OWNER_CITIZEN_BELOW_18M",
				Text = "QUESTION_RETAIL_SHOP_OWNER_CITIZEN_BELOW_18M"
			};

			var qShopOwner_CitizenAbove18m = new Choice
			{
				SystemName = "QUESTION_RETAIL_SHOP_OWNER_CITIZEN_ABOVE_18M",
				Text = "QUESTION_RETAIL_SHOP_OWNER_CITIZEN_ABOVE_18M"
			};
			var qShopOwner_JuristicBelow60m = new Choice
			{
				SystemName = "QUESTION_RETAIL_SHOP_OWNER_JURISTIC_BELOW_60M",
				Text = "QUESTION_RETAIL_SHOP_OWNER_JURISTIC_BELOW_60M"
			};
			var qShopOwner_JuristicAbove60m = new Choice
			{
				SystemName = "QUESTION_RETAIL_SHOP_OWNER_JURISTIC_ABOVE_60M",
				Text = "QUESTION_RETAIL_SHOP_OWNER_JURISTIC_ABOVE_60M"
			};
			var qShopOwner_OTOP = new Choice
			{
				SystemName = "QUESTION_RETAIL_SHOP_OWNER_OTOP",
				Text = "QUESTION_RETAIL_SHOP_OWNER_OTOP"
			};
			qShopOwner.Choices.AddRange(new Choice[]
			{
				qShopOwner_CitizenBelow18m,
				qShopOwner_CitizenAbove18m,
				qShopOwner_JuristicBelow60m,
				qShopOwner_JuristicAbove60m,
				qShopOwner_OTOP,
			});

			var qTeleSale = new Question
			{
				SystemName = "QUESTION_RETAIL_TELESALE",
				Text = "QUESTION_RETAIL_TELESALE",
				Info = "QUESTION_RETAIL_TELESALE_INFO",
			};
			var qTeleSale_yes = new Choice
			{
				SystemName = "YES",
				Text = "QUESTION_RETAIL_TELESALE__YES"
			};
			var qTeleSale_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_RETAIL_TELESALE__NO"
			};
			qTeleSale.Choices.AddRange(new Choice[]
			{
				qTeleSale_yes,
				qTeleSale_no
			});


			var qStoreGas = new Question
			{
				SystemName = "QUESTION_RETAIL_STORE_GAS",
				Text = "QUESTION_RETAIL_STORE_GAS",
				Icon = "~/img/SmartQuiz/10 gas.svg",
			};
			var qStoreGas_no = new Choice
			{
				SystemName = "QUESTION_RETAIL_STORE_GAS__CHOICE_NO",
				Text = "QUESTION_RETAIL_STORE_GAS__CHOICE_NO"
			};
			var qStoreGas_le1000kg = new Choice
			{
				SystemName = "QUESTION_RETAIL_STORE_GAS__CHOICE_LE1000KG",
				Text = "QUESTION_RETAIL_STORE_GAS__CHOICE_LE1000KG"
			};
			var qStoreGas_gt1000kg = new Choice
			{
				SystemName = "QUESTION_RETAIL_STORE_GAS__CHOICE_GT1000KG",
				Text = "QUESTION_RETAIL_STORE_GAS__CHOICE_GT1000KG"
			};

			qStoreGas.Choices.Add(qStoreGas_no);
			qStoreGas.Choices.Add(qStoreGas_le1000kg);
			qStoreGas.Choices.Add(qStoreGas_gt1000kg);
			quizRetail.Questions.Add(qStoreGas);


			var qAgent = new Question
			{
				SystemName = "QUESTION_RETAIL_AGENT",
				Text = "QUESTION_RETAIL_AGENT",
				Info = "QUESTION_RETAIL_AGENT_INFO",
			};
			var qAgent_yes = new Choice
			{
				SystemName = "YES",
				Text = "QUESTION_RETAIL_AGENT__YES"
			};
			var qAgent_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_RETAIL_AGENT__NO"
			};
			qAgent.Choices.AddRange(new Choice[]
			{
				qAgent_yes,
				qAgent_no
			});
			qShopOwner_CitizenAbove18m.NextQuestionID = qTeleSale.QuestionID.ToString();
			qShopOwner_JuristicAbove60m.NextQuestionID = qTeleSale.QuestionID + "," + qAgent.QuestionID;
			qShopOwner_JuristicBelow60m.NextQuestionID = qAgent.QuestionID.ToString();
			qShopOwner_OTOP.NextQuestionID = qAgent.QuestionID.ToString();
			qShopOwner.SubQuestions = new List<Question> {
				qTeleSale,
				qAgent,
			};

			quizRetail.Questions.Add(qShopOwner);

			// Add sign-tax question
			AddSignTaxQuestion(quizRetail, BUSINESS_RETAIL_TYPE);

			// Add construction question
			AddShopConstructionQuestion(quizRetail, BUSINESS_RETAIL_TYPE);

			db.InsertOne(quizRetail);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = "ขอใบอนุญาตจำหน่ายปุ๋ย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_fertilizer.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = "ขอใบอนุญาตจำหน่ายเมล็ดพันธุ์ควบคุมเพื่อการค้า",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_seed.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_ANIMAL_FOOD,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_animalFood.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_ANIMAL,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_animal.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_CARCASS,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_carcasses1.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_carcasses2.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_carcasses3.SystemName, "on"),
						}),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = "ขอใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายภาพยนตร์",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_DVD.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = "ขอใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายวีดิทัศน์",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_Karaoke.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_DIRECT_SELL,
					DisplayConditions =  new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qShopOwner.SystemName, qShopOwner_JuristicAbove60m.SystemName),
							new QuizAnswer(qShopOwner.SystemName, qShopOwner_JuristicBelow60m.SystemName),
							new QuizAnswer(qShopOwner.SystemName, qShopOwner_OTOP.SystemName),
						}),
						new QuizAnswer(qAgent.SystemName, qAgent_yes.SystemName)
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_DIRECT_MARKETING,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qShopOwner.SystemName, qShopOwner_JuristicAbove60m.SystemName),
							new QuizAnswer(qShopOwner.SystemName, qShopOwner_CitizenAbove18m.SystemName),
						}),
						new QuizAnswer(qTeleSale.SystemName, qTeleSale_yes.SystemName)
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_TOBACCO,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_tobacco.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_COSMETIC,//"ขอจดแจ้งรายละเอียดการผลิตเพื่อขายหรือนำเข้าเพื่อขายเครื่องสำอางควบคุม",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qCosmetic.SystemName, qCosmetic_yes.SystemName),
						new QuizAnswer(qCosmeticPermit.SystemName, qCosmeticPermit_no.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = "ขอใบอนุญาตขายยาแผนปัจจุบัน",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_medicine.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_ALCOHOL,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_alcohol.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_LT_200,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
						new QuizAnswer(qArea.SystemName, qArea_small.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_FOOD_GE_200,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
						new QuizAnswer(qArea.SystemName, qArea_large.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					//AppSystemName = "จดทะเบียนพาณิชย์",
					AppSystemName = AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
					DisplayConditions = new QuizAnswer[0],
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},

				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_STORE_GAS_LE1000KG,
					DisplayConditions = new QuizAnswer[]
					{
						new QuizAnswer(qStoreGas.SystemName, qStoreGas_le1000kg.SystemName)
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_STORE_GAS_GT1000KG,
					DisplayConditions = new QuizAnswer[]
					{
						new QuizAnswer(qStoreGas.SystemName, qStoreGas_gt1000kg.SystemName)
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},

				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SELL_CARD,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_card.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_RETAIL_TYPE,
					AppSystemName = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต สะสม บรรจุ ขนส่งสารกำจัดศัตรูพืชหรือพาหะนำโรค",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qProduct.SystemName + "_" + qProduct_insecticide.SystemName, "on"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Add sign-tax app mapping
			items = AddSignTaxAppMappings(items, BUSINESS_RETAIL_TYPE);

			// Add construction app mapping
			items = AddShopConstructionAppMappings(items, BUSINESS_RETAIL_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitUtilitiesQuiz()
		{
			string BUSINESS_UTILITIES_TYPE = BusinessGroupID.UTILITIES;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizRetail = new SmartQuiz(BUSINESS_UTILITIES_TYPE)
			{
				Text = "QUIZ_OPEN_UTILITIES"
			};

			#region [Q1. มีเลบประจำบ้าน 11 หลัก]
			var qHomeIdentity = new Question()
			{
				SystemName = "HAS_HOME_NUMBER",
				Text = "QUESTION_UTILITIES_HOME_NUMBER",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qHomeIdentity_yes = new Choice()
			{
				SystemName = "YES",
				Text = "QUESTION_UTILITIES_HAS_HOME_NUMBER_YES"
			};
			var qHomeIdentity_no = new Choice()
			{
				SystemName = "NO",
				Text = "QUESTION_UTILITIES_HOME_NUMBER_NO"
			};
			qHomeIdentity.Choices.Add(qHomeIdentity_yes);
			qHomeIdentity.Choices.Add(qHomeIdentity_no);
			quizRetail.Questions.Add(qHomeIdentity);
			#endregion

			#region [Q2. สถานที่ที่ต้องการขอติดตั้งสาธารณูปโภคอยู่ภายในเขต]
			var qMetroOrProvince = new Question()
			{
				SystemName = "METRO_OR_PROVINCE",
				Text = "QUESTION_METRO_OR_PROVINCE",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qMetroOrProvince_metro = new Choice()
			{
				SystemName = "METRO",
				Text = "QUESTION_METRO_OR_PROVINCE_METRO"
			};
			var qMetroOrProvince_province = new Choice()
			{
				SystemName = "PROVINCE",
				Text = "QUESTION_METRO_OR_PROVINCE_PROVINCE"
			};
			qMetroOrProvince.Choices.Add(qMetroOrProvince_metro);
			qMetroOrProvince.Choices.Add(qMetroOrProvince_province);
			quizRetail.Questions.Add(qMetroOrProvince);
			#endregion

			#region [Q3. บริการที่ต้องการขอติดตั้ง นครหลวง]
			var qUtilityServices = new Question()
			{
				SystemName = "UTILITY_SERVICES",
				Text = "QUESTION_UTILITY_SERVICES",
				Icon = "~/img/SmartQuiz/4 shop.svg",
				AllowMultipleAnswer = true
			};
			var qUtilityServices_Electric = new Choice()
			{
				SystemName = "ELECTRIC",
				Text = "QUESTION_UTILITY_SERVICES_ELECTRIC"
			};
			var qUtilityServices_Water = new Choice()
			{
				SystemName = "WATER",
				Text = "QUESTION_UTILITY_SERVICES_WATER"
			};
			var qUtilityServices_Phone = new Choice()
			{
				SystemName = "PHONE",
				Text = "QUESTION_UTILITY_SERVICES_PHONE"
			};
			qUtilityServices.Choices.Add(qUtilityServices_Electric);
			qUtilityServices.Choices.Add(qUtilityServices_Water);
			qUtilityServices.Choices.Add(qUtilityServices_Phone);
			quizRetail.Questions.Add(qUtilityServices);
			#endregion

			#region [Q4. ประเภทการติดตั้ง]
			var qInstallType = new Question()
			{
				SystemName = "INSTALL_TYPE",
				Text = "QUESTION_INSTALL_TYPE",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qInstallType_Permanent = new Choice()
			{
				SystemName = "PERMANENT",
				Text = "QUESTION_INSTALL_TYPE_PERMANENT"
			};
			var qInstallType_Temporarily = new Choice()
			{
				SystemName = "TEMPORARILY",
				Text = "QUESTION_INSTALL_TYPE_TEMPORARILY"
			};
			qInstallType.Choices.Add(qInstallType_Permanent);
			qInstallType.Choices.Add(qInstallType_Temporarily);
			quizRetail.Questions.Add(qInstallType);
			#endregion

			QuizAppMapping[] items = new QuizAppMapping[]
				{
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_UTILITIES_TYPE,
						AppSystemName = "แบบฟอร์มขอใช้ไฟฟ้า",
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_metro.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Electric.SystemName, "on")
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
							new QuizAnswer(qHomeIdentity.SystemName, new string[] { qHomeIdentity_yes.SystemName }, "VAL_HOME_NUMBER_REQUIRED"),
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_metro.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Electric.SystemName, "on"),
							new QuizAnswer(qInstallType.SystemName, new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_UTILITIES_TYPE,
						AppSystemName = "แบบฟอร์มขอใช้น้ำประปา",
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_metro.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Water.SystemName, "on")
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
							new QuizAnswer(qHomeIdentity.SystemName, new string[] { qHomeIdentity_yes.SystemName }, "VAL_HOME_NUMBER_REQUIRED"),
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_metro.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Water.SystemName, "on"),
							new QuizAnswer(qInstallType.SystemName, new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_UTILITIES_TYPE,
						AppSystemName = "ขอใช้บริการโทรศัพท์พื้นฐาน และอินเทอร์เน็ต",
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_metro.SystemName, qMetroOrProvince_province.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Phone.SystemName, "on")
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
							new QuizAnswer(qHomeIdentity.SystemName, new string[] { qHomeIdentity_yes.SystemName }, "VAL_HOME_NUMBER_REQUIRED"),
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_metro.SystemName, qMetroOrProvince_province.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Phone.SystemName, "on"),
							new QuizAnswer(qInstallType.SystemName, new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_UTILITIES_TYPE,
						AppSystemName = "แบบฟอร์มขอใช้ไฟฟ้า (ภูมิภาค)",
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_province.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Electric.SystemName, "on")
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
							new QuizAnswer(qHomeIdentity.SystemName, new string[] { qHomeIdentity_yes.SystemName }, "VAL_HOME_NUMBER_REQUIRED"),
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_province.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Electric.SystemName, "on"),
							new QuizAnswer(qInstallType.SystemName, new string[] { "PERMANENT" }, "VAL_ONLY_PERMANENT")
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_UTILITIES_TYPE,
						AppSystemName = "แบบฟอร์มขอใช้น้ำประปา (ภูมิภาค)",
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_province.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Water.SystemName, "on")
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
							new QuizAnswer(qHomeIdentity.SystemName, new string[] { qHomeIdentity_yes.SystemName }, "VAL_HOME_NUMBER_REQUIRED"),
							new QuizAnswer(qMetroOrProvince.SystemName, qMetroOrProvince_province.SystemName),
							new QuizAnswer(qUtilityServices.SystemName + "_" + qUtilityServices_Water.SystemName, "on"),
							new QuizAnswer(qInstallType.SystemName, new string[] { "PERMANENT", "TEMPORARILY" })
						}
					}
				};

			db.InsertOne(quizRetail);
			MongoFactory.GetQuizAppMappingCollection().InsertMany(items);
		}

		private static void InitStartingBusinessQuiz()
		{
			string BUSINESS_UTILITIES_TYPE = BusinessGroupID.STARTINGBUSINESS;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizStarting = new SmartQuiz(BUSINESS_UTILITIES_TYPE)
			{
				Text = "QUIZ_OPEN_STARTINGBUSINESS"
			};

			#region [Q1. คุณได้จดทะเบียนนิติบุคคลกับกรมพัฒนาธุรกิจการค้าจนเสร็จสิ้นแล้วหรือไม่]
			var qIsRegisteredJuristic = new Question()
			{
				SystemName = "IS_REGISTERED_JURISTIC",
				Text = "QUESTION_STARTINGBUSINESS_IS_REGISTERED_JURISTIC",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qHomeIdentity_yes = new Choice()
			{
				SystemName = "YES",
				Text = "QUESTION_STARTINGBUSINESS_IS_REGISTERED_JURISTIC_YES"
			};
			var qHomeIdentity_no = new Choice()
			{
				SystemName = "NO",
				Text = "QUESTION_STARTINGBUSINESS_IS_REGISTERED_JURISTIC_NO"
			};
			qIsRegisteredJuristic.Choices.Add(qHomeIdentity_yes);
			qIsRegisteredJuristic.Choices.Add(qHomeIdentity_no);
			quizStarting.Questions.Add(qIsRegisteredJuristic);
			#endregion

			#region [Q2. กิจการของคุณมีการค้าขายสินค้าหรือไม่]
			var qIsBusinessSellingGoods = new Question()
			{
				SystemName = "IS_BUSINESS_SELLING_GOODS",
				Text = "QUESTION_STARTINGBUSINESS_IS_BUSINESS_SELLING_GOODS",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qIsBusinessSellingGoods_yes = new Choice()
			{
				SystemName = "YES",
				Text = "QUESTION_STARTINGBUSINESS_IS_BUSINESS_SELLING_GOODS_YES"
			};
			var qIsBusinessSellingGoods_no = new Choice()
			{
				SystemName = "NO",
				Text = "QUESTION_STARTINGBUSINESS_IS_BUSINESS_SELLING_GOODS_NO"
			};
			qIsBusinessSellingGoods.Choices.Add(qIsBusinessSellingGoods_yes);
			qIsBusinessSellingGoods.Choices.Add(qIsBusinessSellingGoods_no);
			quizStarting.Questions.Add(qIsBusinessSellingGoods);
			#endregion

			#region [Q2.1 ธุรกิจของคุณเข้าข่ายประเภทกิจการใดบ้าง (เลือกได้มากกว่า 1 ข้อ)]
			var qGoodTypes = new Question()
			{
				SystemName = "QUESTION_STARTINGBUSINESS_BUSINESS_GOODS_TYPE",
				Text = "QUESTION_STARTINGBUSINESS_BUSINESS_GOODS_TYPE",
				AllowMultipleAnswer = true
			};
			string[] goodTypes = new string[] { "DOMESTIC_AGRICULTURAL_CROPS","DOMESTIC_ANIMALS_AND_UNPROCESSED_MEAT", "DOMESTIC_FERTILIZERS_CHEMICALS_MEDICINES" ,"DOMESTIC_ANIMAL_FOODS", "DOMESTIC_NEWSPAPER_MAGAZINES_AND_TEXTBOOKS",
			"ALL_5_DOMESTIC_GOODS", "EXPORTED_GOODS_BY_INDUSTRIAL_PARK", "CDS_DVDS_TAPES_FOR_SELL_OR_RENT", "GEMSTONE_AND_JEWELRY", "RELIGION_AND_DOMESTIC_CHARITY", "GOODS_AND_SERVICES_FOR_MINISTRY",
			"DECREE_NO239_GOODS_AND_SERVICES", "IVORY_AND_CARVED_IVORY" , "GOOD_TYPES_ETC"};
			foreach (var gt in goodTypes)
			{
				var qGoodTypeC = new Choice()
				{
					SystemName = gt,
					Text = "QUESTION_STARTINGBUSINESS_BUSINESS_GOODS_TYPE_" + gt
				};
				qGoodTypes.Choices.Add(qGoodTypeC);
			}

			qIsBusinessSellingGoods.SubQuestions.Add(qGoodTypes);
			qIsBusinessSellingGoods_yes.NextQuestionID = qGoodTypes.QuestionID.ToString();
			#endregion

			#region [Q3. กิจการของคุณมีการให้บริการหรือไม่]
			var qIsBusinessProvideServices = new Question()
			{
				SystemName = "IS_BUSINESS_PROVIDE_SERVICES",
				Text = "QUESTION_STARTINGBUSINESS_IS_BUSINESS_PROVIDE_SERVICES",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qIsBusinessProvideServices_yes = new Choice()
			{
				SystemName = "YES",
				Text = "QUESTION_STARTINGBUSINESS_IS_BUSINESS_PROVIDE_SERVICES_YES"
			};
			var qIsBusinessProvideServices_no = new Choice()
			{
				SystemName = "NO",
				Text = "QUESTION_STARTINGBUSINESS_IS_BUSINESS_PROVIDE_SERVICES_NO"
			};
			qIsBusinessProvideServices.Choices.Add(qIsBusinessProvideServices_yes);
			qIsBusinessProvideServices.Choices.Add(qIsBusinessProvideServices_no);
			quizStarting.Questions.Add(qIsBusinessProvideServices);
			#endregion

			#region [Q3.1 ธุรกิจของคุณเข้าข่ายประเภทกิจการใดบ้าง (เลือกได้มากกว่า 1 ข้อ)]
			var qServiceTypes = new Question()
			{
				SystemName = "QUESTION_STARTINGBUSINESS_BUSINESS_SERVICES_TYPE",
				Text = "QUESTION_STARTINGBUSINESS_BUSINESS_SERVICES_TYPE",
				AllowMultipleAnswer = true
			};
			string[] serviceTypes = new string[] { "DOMESTIC_FUEL_TRANSPORTATION", "DOMESTIC_TRANSPORTATION_BY_LANDWATER", "DOMESTIC_TRANSPORTATION_BY_AIR", "INTERNATIONAL_SHIPPING_BY_LANDWATER",
			"REAL_ESTATE_REANTAL_SERVICE", "CULTURAL_AND_ART_SERVICES", "COMPETITIVE_SPORTS_SERVICE", "EDUCATIONAL_SERVICES", "LABOR_CONTRACT_SERVICES", "MEDICAL_TREATMENT_AUDIT_AND_LAWYER",
			"HOSPITAL_SERVICES", "LIBRARY_MUSEUM_ZOO", "KARAOKE_GAME_CONSOLE_SERVICES", "DATACENTER_AND_HOST_SERVICES", "SERVICE_TYPES_ETC" };
			foreach (var st in serviceTypes)
			{
				var qServiceTypeC = new Choice()
				{
					SystemName = st,
					Text = "QUESTION_STARTINGBUSINESS_BUSINESS_SERVICES_TYPE_" + st
				};
				qServiceTypes.Choices.Add(qServiceTypeC);
			}

			qIsBusinessProvideServices.SubQuestions.Add(qServiceTypes);
			qIsBusinessProvideServices_yes.NextQuestionID = qServiceTypes.QuestionID.ToString();
			#endregion

			#region [Q4. ธุรกิจของคุณขายสินค้าหรือให้บริการผ่านอินเทอร์เน็ตหรือไม่]
			var qHasOnlineStore = new Question()
			{
				SystemName = "HAS_ONLINE_STORE",
				Text = "QUESTION_STARTINGBUSINESS_HAS_ONLINE_STORE",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qHasOnlineStore_yes = new Choice()
			{
				SystemName = "YES",
				Text = "QUESTION_STARTINGBUSINESS_HAS_ONLINE_STORE_YES"
			};
			var qHasOnlineStore_no = new Choice()
			{
				SystemName = "NO",
				Text = "QUESTION_STARTINGBUSINESS_HAS_ONLINE_STORE_NO"
			};
			qHasOnlineStore.Choices.Add(qHasOnlineStore_yes);
			qHasOnlineStore.Choices.Add(qHasOnlineStore_no);
			quizStarting.Questions.Add(qHasOnlineStore);
			#endregion

			#region [Q5. ธุรกิจของคุณมีลูกจ้างกี่คน]
			var qNumberOfEmp = new Question()
			{
				SystemName = "NUMBER_OF_EMPLOYEES",
				Text = "QUESTION_STARTINGBUSINESS_NUMBER_OF_EMPLOYEES",
				Icon = "~/img/SmartQuiz/4 shop.svg"
			};
			var qNumberOfEmp_no = new Choice()
			{
				SystemName = "01",
				Text = "QUESTION_STARTINGBUSINESS_NUMBER_OF_EMPLOYEES_01"
			};
			var qNumberOfEmp_1_to_9 = new Choice()
			{
				SystemName = "02",
				Text = "QUESTION_STARTINGBUSINESS_NUMBER_OF_EMPLOYEES_02"
			};
			var qNumberOfEmp_mt_10 = new Choice()
			{
				SystemName = "03",
				Text = "QUESTION_STARTINGBUSINESS_NUMBER_OF_EMPLOYEES_03"
			};
			qNumberOfEmp.Choices.Add(qNumberOfEmp_no);
			qNumberOfEmp.Choices.Add(qNumberOfEmp_1_to_9);
			qNumberOfEmp.Choices.Add(qNumberOfEmp_mt_10);
			quizStarting.Questions.Add(qNumberOfEmp);
			#endregion

			QuizAppMapping[] items = new QuizAppMapping[]
				{
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_UTILITIES_TYPE,
						AppSystemName = "การขอขึ้นทะเบียนนายจ้างและผู้ประกันตน",
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qIsRegisteredJuristic.SystemName, new string[] { qHomeIdentity_yes.SystemName, qHomeIdentity_no.SystemName }, "VAL_DBD_REGIS_REQUIRED"),
							new QuizAnswer(qNumberOfEmp.SystemName , new string[] { qNumberOfEmp_1_to_9.SystemName, qNumberOfEmp_mt_10.SystemName })
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
							new QuizAnswer(qIsRegisteredJuristic.SystemName, new string[] { qHomeIdentity_yes.SystemName }, "VAL_DBD_REGIS_REQUIRED"),
							new QuizAnswer(qNumberOfEmp.SystemName , new string[] { qNumberOfEmp_1_to_9.SystemName, qNumberOfEmp_mt_10.SystemName })
						}
					},
					new QuizAppMapping()
					{
						QuizGroup = BUSINESS_UTILITIES_TYPE,
						AppSystemName = "แบบฟอร์มขอจดทะเบียนภาษีมูลค่าเพิ่ม",
						DisplayConditions = new QuizAnswer[]
						{
							new QuizAnswer(qIsRegisteredJuristic.SystemName, new string[] { qHomeIdentity_yes.SystemName, qHomeIdentity_no.SystemName }, "VAL_DBD_REGIS_REQUIRED")
						},
						OnlineRequestAllowedConditions = new QuizAnswer[]
						{
							new QuizAnswer(qIsRegisteredJuristic.SystemName, new string[] { qHomeIdentity_yes.SystemName }, "VAL_DBD_REGIS_REQUIRED")
						}
					}
				};

			db.InsertOne(quizStarting);
			MongoFactory.GetQuizAppMappingCollection().InsertMany(items);
		}

		private static void InitAnimalQuiz()
		{
			string BUSINESS_ANIMAL_TYPE = BusinessGroupID.VETERINARY;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizAnimal = new SmartQuiz(BUSINESS_ANIMAL_TYPE)
			{
				Text = "QUIZ_OPEN_ANIMAL"
			};

			// 1
			var qSellFood = new Question
			{
				SystemName = "QUESTION_ANIMAL_SELL_FOOD",
				Text = "QUESTION_ANIMAL_SELL_FOOD",
				Icon = "~/img/SmartQuiz/17 animal.svg",
			};
			var qSellFood_yes = new Choice
			{
				SystemName = "QUESTION_ANIMAL_SELL_FOOD__YES",
				Text = "QUESTION_ANIMAL_SELL_FOOD__YES",
			};
			var qSellFood_no = new Choice
			{
				SystemName = "QUESTION_ANIMAL_SELL_FOOD__NO",
				Text = "QUESTION_ANIMAL_SELL_FOOD__NO",
			};

			qSellFood.Choices.AddRange(new Choice[]
			{
				qSellFood_yes,
				qSellFood_no,
			});
			quizAnimal.Questions.Add(qSellFood);

			// 2
			var qSignTax = new Question
			{
				SystemName = "QUESTION_ANIMAL_SIGN_TAX",
				Text = "QUESTION_ANIMAL_SIGN_TAX",
			};
			var qSignTax_yes = new Choice
			{
				SystemName = "QUESTION_ANIMAL_SIGN_TAX__YES",
				Text = "QUESTION_ANIMAL_SIGN_TAX__YES",
			};
			var qSignTax_no = new Choice
			{
				SystemName = "QUESTION_ANIMAL_SIGN_TAX__NO",
				Text = "QUESTION_ANIMAL_SIGN_TAX__NO",
			};
			qSignTax.Choices.AddRange(new Choice[]
			{
				qSignTax_yes,
				qSignTax_no,
			});
			quizAnimal.Questions.Add(qSignTax);

			// Add construction question
			AddShopConstructionQuestion(quizAnimal, BUSINESS_ANIMAL_TYPE);

			db.InsertOne(quizAnimal);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ANIMAL_TYPE,
					AppSystemName = "ขอใบอนุญาตขายอาหารสัตว์ควบคุมเฉพาะ",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ANIMAL_TYPE,
					AppSystemName = "ยื่นชำระภาษีป้าย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSignTax.SystemName, qSignTax_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ANIMAL_TYPE,
					AppSystemName = "ขอหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer("id", BUSINESS_ANIMAL_TYPE), // Always include this application
                    },
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ANIMAL_TYPE,
					AppSystemName = "ขอใบอนุญาตตั้งสถานพยาบาลสัตว์",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer("id", BUSINESS_ANIMAL_TYPE), // Always include this application
                    },
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ANIMAL_TYPE,
					AppSystemName = "ขอใบอนุญาตดำเนินการสถานพยาบาลสัตว์",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer("id", BUSINESS_ANIMAL_TYPE), // Always include this application
                    },
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Add construction app mapping
			items = AddShopConstructionAppMappings(items, BUSINESS_ANIMAL_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitConstructionQuiz()
		{
			string BUSINESS_CONSTRUCTION_TYPE = BusinessGroupID.CONSTRUCTION;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizConstruction = new SmartQuiz(BUSINESS_CONSTRUCTION_TYPE)
			{
				Text = "QUIZ_OPEN_CONSTRUCTION"
			};

			// 1
			var qType = new Question
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE",
				Text = "QUESTION_CONSTRUCTION_TYPE",
				Icon = "~/img/SmartQuiz/10 shop.svg",
				AllowMultipleAnswer = true
			};

			var qType_extraLarge = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__EXTRA_LARGE",
				Text = "QUESTION_CONSTRUCTION_TYPE__EXTRA_LARGE",
			};

			var qType_high = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__HIGH",
				Text = "QUESTION_CONSTRUCTION_TYPE__HIGH",
			};

			var qType_oil = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__OIL",
				Text = "QUESTION_CONSTRUCTION_TYPE__OIL",
			};

			var qType_public = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__PUBLIC",
				Text = "QUESTION_CONSTRUCTION_TYPE__PUBLIC",
			};

			var qType_none = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__NONE",
				Text = "QUESTION_CONSTRUCTION_TYPE__NONE",
			};

			qType.Choices.AddRange(new Choice[]
			{
				qType_extraLarge,
				qType_high,
				qType_oil,
				qType_public,
				qType_none
			});
			quizConstruction.Questions.Add(qType);

			// 1.1
			var qIsLarge = new Question
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE",
				Text = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE",
			};
			var qIsLarge_yes = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__YES",
				Text = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__YES",
			};
			var qIsLarge_no = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__NO",
				Text = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__NO",
			};

			qIsLarge.Choices.AddRange(new Choice[]
			{
				qIsLarge_yes,
				qIsLarge_no,
			});
			qType.SubQuestions.Add(qIsLarge);
			qType_none.NextQuestionID = qIsLarge.QuestionID.ToString();



			// 1.2
			var qOrgPlace = new Question
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE",
				Text = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE",
			};
			var qOrgPlace_bkk = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__BANGKOK",
				Text = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__BANGKOK",
			};
			var qOrgPlace_local = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__LOCAL",
				Text = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__LOCAL",
			};

			qOrgPlace.Choices.AddRange(new Choice[]
			{
				qOrgPlace_bkk,
				qOrgPlace_local,
			});
			qIsLarge.SubQuestions.Add(qOrgPlace);
			qIsLarge_yes.NextQuestionID = qOrgPlace.QuestionID.ToString();


			// 2
			var qMixedMaterial = new Question
			{
				SystemName = "QUESTION_CONSTRUCTION_MIXED_MATERIAL",
				Text = "QUESTION_CONSTRUCTION_MIXED_MATERIAL",
				Icon = "~/img/SmartQuiz/10 shop.svg",
			};
			var qMixedMaterial_yes = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_MIXED_MATERIAL__YES",
				Text = "QUESTION_CONSTRUCTION_MIXED_MATERIAL__YES",
			};
			var qMixedMaterial_no = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_MIXED_MATERIAL__NO",
				Text = "QUESTION_CONSTRUCTION_MIXED_MATERIAL__NO",
			};
			qMixedMaterial.Choices.AddRange(new Choice[]
			{
				qMixedMaterial_yes,
				qMixedMaterial_no,
			});
			quizConstruction.Questions.Add(qMixedMaterial);

			// Append sign-tax questions
			AddSignTaxQuestion(quizConstruction, BUSINESS_CONSTRUCTION_TYPE);

			db.InsertOne(quizConstruction);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_CONSTRUCTION_TYPE,
					AppSystemName = "ใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีพิเศษ",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qType.SystemName + "_" + qType_extraLarge.SystemName, "on"),
							new QuizAnswer(qType.SystemName + "_" + qType_high.SystemName, "on"),
							new QuizAnswer(qType.SystemName + "_" + qType_oil.SystemName, "on"),
							new QuizAnswer(qType.SystemName + "_" + qType_public.SystemName, "on"),
						})
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_CONSTRUCTION_TYPE,
					AppSystemName = "ใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอื่นๆ",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qType.SystemName + "_" + qType_none.SystemName, "on"),
						new QuizAnswer(qIsLarge.SystemName, qIsLarge_no.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_CONSTRUCTION_TYPE,
					AppSystemName = "ขอใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักการโยธา",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qType.SystemName + "_" + qType_none.SystemName, "on"),
						new QuizAnswer(qIsLarge.SystemName, qIsLarge_yes.SystemName),
						new QuizAnswer(qOrgPlace.SystemName, qOrgPlace_bkk.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_CONSTRUCTION_TYPE,
					AppSystemName = "ขอใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักงานเขต",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qType.SystemName + "_" + qType_none.SystemName, "on"),
						new QuizAnswer(qIsLarge.SystemName, qIsLarge_yes.SystemName),
						new QuizAnswer(qOrgPlace.SystemName, qOrgPlace_local.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_CONSTRUCTION_TYPE,
					AppSystemName = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การสะสม ผสมซีเมนต์ หิน ดิน ทราย วัสดุก่อสร้าง รวมทั้งการขุด ตัก ดูด โม่ บด ย่อย ด้วยเครื่องจักร",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qMixedMaterial.SystemName, qMixedMaterial_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_CONSTRUCTION_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitElectronicQuiz()
		{
			string BUSINESS_ELECT_TYPE = BusinessGroupID.ELECTRONIC;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizElect = new SmartQuiz(BUSINESS_ELECT_TYPE)
			{
				Text = "QUIZ_OPEN_ELECTRONIC"
			};

			var qProduct = new Question
			{
				SystemName = "QUESTION_ELECTRONIC_PRODUCT",
				Text = "QUESTION_ELECTRONIC_PRODUCT",
				Icon = "~/img/SmartQuiz/10 shop.svg",
			};
			var qProduct_all = new Choice
			{
				SystemName = "QUESTION_ELECTRONIC_PRODUCT__ALL",
				Text = "QUESTION_ELECTRONIC_PRODUCT__ALL",
			};
			var qProduct_repair = new Choice
			{
				SystemName = "QUESTION_ELECTRONIC_PRODUCT__REPAIR",
				Text = "QUESTION_ELECTRONIC_PRODUCT__REPAIR",
			};
			var qProduct_sell = new Choice
			{
				SystemName = "QUESTION_ELECTRONIC_PRODUCT__SELL",
				Text = "QUESTION_ELECTRONIC_PRODUCT__SELL",
			};
			qProduct.Choices.AddRange(new Choice[]
			{
				qProduct_all,
				qProduct_repair,
				qProduct_sell
			});
			quizElect.Questions.Add(qProduct);

			var qSignTax = new Question
			{
				SystemName = "QUESTION_ELECTRONIC_SIGN_TAX",
				Text = "QUESTION_ELECTRONIC_SIGN_TAX",
			};
			var qSignTax_yes = new Choice
			{
				SystemName = "QUESTION_ELECTRONIC_SIGN_TAX__YES",
				Text = "QUESTION_ELECTRONIC_SIGN_TAX__YES",
			};
			//var qSignTax_no = new Choice
			//{
			//    SystemName = "QUESTION_ELECTRONIC_SIGN_TAX__NO",
			//    Text = "QUESTION_ELECTRONIC_SIGN_TAX__NO",
			//};
			qSignTax.Choices.AddRange(new Choice[]
			{
				qSignTax_yes,
                //qSignTax_no,
            });
			quizElect.Questions.Add(qSignTax);

			// Add construction question
			AddShopConstructionQuestion(quizElect, BUSINESS_ELECT_TYPE);

			db.InsertOne(quizElect);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ELECT_TYPE,
					AppSystemName = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต ซ่อมเครื่องอิเล็กทรอนิกส์ เครื่องไฟฟ้า อุปกรณ์อิเล็กทรอนิกส์ หรืออุปกรณ์ไฟฟ้า",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qProduct.SystemName, qProduct_all.SystemName),
						new QuizAnswer(qProduct.SystemName, qProduct_repair.SystemName)
						})
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ELECT_TYPE,
					AppSystemName = "ใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qProduct.SystemName, qProduct_all.SystemName),
							new QuizAnswer(qProduct.SystemName, qProduct_repair.SystemName),
							new QuizAnswer(qProduct.SystemName, qProduct_sell.SystemName),
						})
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ELECT_TYPE,
					AppSystemName = "ยื่นชำระภาษีป้าย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSignTax.SystemName, qSignTax_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Add construction app mapping
			items = AddShopConstructionAppMappings(items, BUSINESS_ELECT_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitCarCareQuiz()
		{
			string BUSINESS_CARCARE_TYPE = BusinessGroupID.CARCARE;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizCarCare = new SmartQuiz(BUSINESS_CARCARE_TYPE)
			{
				Text = "QUIZ_OPEN_CARCARE"
			};

			var qProduct = new Question
			{
				SystemName = "QUESTION_CARCARE_PRODUCT",
				Text = "QUESTION_CARCARE_PRODUCT",
				Icon = "~/img/SmartQuiz/10 shop.svg",
				AllowMultipleAnswer = true
			};
			var qProduct_wash = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__WASH",
				Text = "QUESTION_CARCARE_PRODUCT__WASH",
			};
			var qProduct_coat = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__COAT",
				Text = "QUESTION_CARCARE_PRODUCT__COAT",
			};
			var qProduct_scrub = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__SCRUB",
				Text = "QUESTION_CARCARE_PRODUCT__SCRUB",
			};
			var qProduct_vacuum = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__VACUUM",
				Text = "QUESTION_CARCARE_PRODUCT__VACUUM",
			};
			var qProduct_purify = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__PURIFY",
				Text = "QUESTION_CARCARE_PRODUCT__PURIFY",
			};
			var qProduct_innerWash = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__INNER_WASH",
				Text = "QUESTION_CARCARE_PRODUCT__INNER_WASH",
			};
			var qProduct_oilChange = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__OIL_CHANGE",
				Text = "QUESTION_CARCARE_PRODUCT__OIL_CHANGE",
			};
			var qProduct_checkup = new Choice
			{
				SystemName = "QUESTION_CARCARE_PRODUCT__OIL_CHECKUP",
				Text = "QUESTION_CARCARE_PRODUCT__OIL_CHECKUP",
			};
			qProduct.Choices.AddRange(new Choice[]
			{
				qProduct_wash,
				qProduct_coat,
				qProduct_scrub,
				qProduct_vacuum,
				qProduct_purify,
				qProduct_innerWash,
				qProduct_oilChange,
				qProduct_checkup
			});
			quizCarCare.Questions.Add(qProduct);

			var qSignTax = new Question
			{
				SystemName = "QUESTION_CARCARE_SIGN_TAX",
				Text = "QUESTION_CARCARE_SIGN_TAX",
			};
			var qSignTax_yes = new Choice
			{
				SystemName = "QUESTION_CARCARE_SIGN_TAX__YES",
				Text = "QUESTION_CARCARE_SIGN_TAX__YES",
			};
			var qSignTax_no = new Choice
			{
				SystemName = "QUESTION_CARCARE_SIGN_TAX__NO",
				Text = "QUESTION_CARCARE_SIGN_TAX__NO",
			};
			qSignTax.Choices.AddRange(new Choice[]
			{
				qSignTax_yes,
				qSignTax_no,
			});
			quizCarCare.Questions.Add(qSignTax);

			// Add construction question
			AddShopConstructionQuestion(quizCarCare, BUSINESS_CARCARE_TYPE);

			db.InsertOne(quizCarCare);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_CARCARE_TYPE,
					AppSystemName = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การล้าง ขัดสี เคลือบสี หรืออัดฉีดยานยนต์",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_wash.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_coat.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_scrub.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_vacuum.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_purify.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_innerWash.SystemName, "on"),
						})
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_CARCARE_TYPE,
					AppSystemName = "ยื่นชำระภาษีป้าย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSignTax.SystemName, qSignTax_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Add construction app mapping
			items = AddShopConstructionAppMappings(items, BUSINESS_CARCARE_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitCoffeeShopQuiz()
		{
			string BUSINESS_COFFEESHOP_TYPE = BusinessGroupID.COFFEESHOP;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizCoffeeShop = new SmartQuiz(BUSINESS_COFFEESHOP_TYPE)
			{
				Text = "QUIZ_OPEN_COFFEESHOP"
			};

			// 1
			var qSellFood = new Question
			{
				SystemName = "QUESTION_COFFEESHOP_SELL_FOOD",
				Text = "QUESTION_COFFEESHOP_SELL_FOOD",
				Icon = "~/img/SmartQuiz/7 food.svg",
			};
			var qSellFood_yes = new Choice
			{
				SystemName = "QUESTION_COFFEESHOP_SELL_FOOD__YES",
				Text = "QUESTION_COFFEESHOP_SELL_FOOD__YES",
			};
			var qSellFood_no = new Choice
			{
				SystemName = "QUESTION_COFFEESHOP_SELL_FOOD__NO",
				Text = "QUESTION_COFFEESHOP_SELL_FOOD__NO",
			};

			qSellFood.Choices.AddRange(new Choice[]
			{
				qSellFood_yes,
				qSellFood_no,
			});
			quizCoffeeShop.Questions.Add(qSellFood);

			// 1.1
			var qSellFoodArea = new Question
			{
				SystemName = "QUESTION_COFFEESHOP_SELL_FOOD_AREA",
				Text = "QUESTION_COFFEESHOP_SELL_FOOD_AREA",
			};
			var qSellFoodArea_LT200 = new Choice
			{
				SystemName = "QUESTION_COFFEESHOP_SELL_FOOD_AREA__LT200",
				Text = "QUESTION_COFFEESHOP_SELL_FOOD_AREA__LT200",
			};
			var qSellFoodArea_GT200 = new Choice
			{
				SystemName = "QUESTION_COFFEESHOP_SELL_FOOD_AREA__GT200",
				Text = "QUESTION_COFFEESHOP_SELL_FOOD_AREA__GT200",
			};

			qSellFoodArea.Choices.AddRange(new Choice[]
			{
				qSellFoodArea_LT200,
				qSellFoodArea_GT200,
			});
			qSellFood.SubQuestions.Add(qSellFoodArea);
			qSellFood_yes.NextQuestionID = qSellFoodArea.QuestionID.ToString();

			// 2
			var qSignTax = new Question
			{
				SystemName = "QUESTION_COFFEESHOP_SIGN_TAX",
				Text = "QUESTION_COFFEESHOP_SIGN_TAX",
			};
			var qSignTax_yes = new Choice
			{
				SystemName = "QUESTION_COFFEESHOP_SIGN_TAX__YES",
				Text = "QUESTION_COFFEESHOP_SIGN_TAX__YES",
			};
			var qSignTax_no = new Choice
			{
				SystemName = "QUESTION_COFFEESHOP_SIGN_TAX__NO",
				Text = "QUESTION_COFFEESHOP_SIGN_TAX__NO",
			};
			qSignTax.Choices.AddRange(new Choice[]
			{
				qSignTax_yes,
				qSignTax_no,
			});
			quizCoffeeShop.Questions.Add(qSignTax);

			// Add construction question
			AddShopConstructionQuestion(quizCoffeeShop, BUSINESS_COFFEESHOP_TYPE);

			db.InsertOne(quizCoffeeShop);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_COFFEESHOP_TYPE,
					AppSystemName = "ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
						new QuizAnswer(qSellFoodArea.SystemName, qSellFoodArea_LT200.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_COFFEESHOP_TYPE,
					AppSystemName = "ขอใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
						new QuizAnswer(qSellFoodArea.SystemName, qSellFoodArea_GT200.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_COFFEESHOP_TYPE,
					AppSystemName = "ยื่นชำระภาษีป้าย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSignTax.SystemName, qSignTax_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Add construction app mapping
			items = AddShopConstructionAppMappings(items, BUSINESS_COFFEESHOP_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitFitnessQuiz()
		{
			string BUSINESS_FITNESS_TYPE = BusinessGroupID.FITNESS;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizFitness = new SmartQuiz(BUSINESS_FITNESS_TYPE)
			{
				Text = "QUIZ_OPEN_FITNESS"
			};

			// 1
			var qSellFood = new Question
			{
				SystemName = "QUESTION_FITNESS_SELL_FOOD",
				Text = "QUESTION_FITNESS_SELL_FOOD",
				Icon = "~/img/SmartQuiz/7 food.svg",
			};
			var qSellFood_yes = new Choice
			{
				SystemName = "QUESTION_FITNESS_SELL_FOOD__YES",
				Text = "QUESTION_FITNESS_SELL_FOOD__YES",
			};
			var qSellFood_no = new Choice
			{
				SystemName = "QUESTION_FITNESS_SELL_FOOD__NO",
				Text = "QUESTION_FITNESS_SELL_FOOD__NO",
			};

			qSellFood.Choices.AddRange(new Choice[]
			{
				qSellFood_yes,
				qSellFood_no,
			});
			quizFitness.Questions.Add(qSellFood);

			// 1.1
			var qSellFoodArea = new Question
			{
				SystemName = "QUESTION_FITNESS_SELL_FOOD_AREA",
				Text = "QUESTION_FITNESS_SELL_FOOD_AREA",
			};
			var qSellFoodArea_LT200 = new Choice
			{
				SystemName = "QUESTION_FITNESS_SELL_FOOD_AREA__LT200",
				Text = "QUESTION_FITNESS_SELL_FOOD_AREA__LT200",
			};
			var qSellFoodArea_GT200 = new Choice
			{
				SystemName = "QUESTION_FITNESS_SELL_FOOD_AREA__GT200",
				Text = "QUESTION_FITNESS_SELL_FOOD_AREA__GT200",
			};

			qSellFoodArea.Choices.AddRange(new Choice[]
			{
				qSellFoodArea_LT200,
				qSellFoodArea_GT200,
			});
			qSellFood.SubQuestions.Add(qSellFoodArea);
			qSellFood_yes.NextQuestionID = qSellFoodArea.QuestionID.ToString();

			// 2
			var qSignTax = new Question
			{
				SystemName = "QUESTION_FITNESS_SIGN_TAX",
				Text = "QUESTION_FITNESS_SIGN_TAX",
			};
			var qSignTax_yes = new Choice
			{
				SystemName = "QUESTION_FITNESS_SIGN_TAX__YES",
				Text = "QUESTION_FITNESS_SIGN_TAX__YES",
			};
			var qSignTax_no = new Choice
			{
				SystemName = "QUESTION_FITNESS_SIGN_TAX__NO",
				Text = "QUESTION_FITNESS_SIGN_TAX__NO",
			};
			qSignTax.Choices.AddRange(new Choice[]
			{
				qSignTax_yes,
				qSignTax_no,
			});
			quizFitness.Questions.Add(qSignTax);

			// Add construction question
			AddShopConstructionQuestion(quizFitness, BUSINESS_FITNESS_TYPE);

			db.InsertOne(quizFitness);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_FITNESS_TYPE,
					AppSystemName = "ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
						new QuizAnswer(qSellFoodArea.SystemName, qSellFoodArea_LT200.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_FITNESS_TYPE,
					AppSystemName = "ขอใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
						new QuizAnswer(qSellFoodArea.SystemName, qSellFoodArea_GT200.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_FITNESS_TYPE,
					AppSystemName = "ยื่นชำระภาษีป้าย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSignTax.SystemName, qSignTax_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_FITNESS_TYPE,
					AppSystemName = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การประกอบกิจการสถานที่ออกกำลังกาย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer("id", BUSINESS_FITNESS_TYPE), // Always include this application
                    },
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Add construction app mapping
			items = AddShopConstructionAppMappings(items, BUSINESS_FITNESS_TYPE);

			dbAppMapping.InsertMany(items);
		}

        private static void InitHotelQuiz()
        {
            string BUSINESS_HOTEL_TYPE = BusinessGroupID.HOTEL;
            var db = MongoFactory.GetSmartQuizCollection();
            var quizHotel = new SmartQuiz(BUSINESS_HOTEL_TYPE)
            {
                Text = "QUIZ_OPEN_HOTEL"
            };
            var qServiceMoreThanMonth = new Question
            {
                SystemName = "QUESTION_HOTEL_SERVICE",
                Text = "QUESTION_HOTEL_SERVICE",
            };
            var qServiceMoreThanMonth_yes = new Choice
            {
                SystemName = "QUESTION_HOTEL_SERVICE__YES",
                Text = "QUESTION_HOTEL_SERVICE__YES",
            };
            var qServiceMoreThanMonth_no = new Choice
            {
                SystemName = "QUESTION_HOTEL_SERVICE__NO",
                Text = "QUESTION_HOTEL_SERVICE__NO",
            };

            qServiceMoreThanMonth.Choices.AddRange(new Choice[]
            {
                qServiceMoreThanMonth_yes,
                qServiceMoreThanMonth_no,
            });
            quizHotel.Questions.Add(qServiceMoreThanMonth);

            var qRooms = new Question
            {
                SystemName = "QUESTION_HOTEL_ROOMS",
                Text = "QUESTION_HOTEL_ROOMS",
            };
            var qRooms_yes = new Choice
            {
                SystemName = "QUESTION_HOTEL_ROOMS__YES",
                Text = "QUESTION_HOTEL_ROOMS__YES",
            };
            var qRooms_no = new Choice
            {
                SystemName = "QUESTION_HOTEL_ROOMS__NO",
                Text = "QUESTION_HOTEL_ROOMS__NO",
            };

            qRooms.Choices.AddRange(new Choice[]
            {
                qRooms_yes,
                qRooms_no,
            });
            quizHotel.Questions.Add(qRooms);

            var qPeopleCanLive = new Question
            {
                SystemName = "QUESTION_HOTEL_PEOPLE_CAN_LIVE",
                Text = "QUESTION_HOTEL_PEOPLE_CAN_LIVE",
            };
            var qPeopleCanLive_yes = new Choice
            {
                SystemName = "QUESTION_HOTEL_PEOPLE_CAN_LIVE__YES",
                Text = "QUESTION_HOTEL_PEOPLE_CAN_LIVE__YES",
            };
            var qPeopleCanLive_no = new Choice
            {
                SystemName = "QUESTION_HOTEL_PEOPLE_CAN_LIVE__NO",
                Text = "QUESTION_HOTEL_PEOPLE_CAN_LIVE__NO",
            };

            qPeopleCanLive.Choices.AddRange(new Choice[]
            {
                qPeopleCanLive_yes,
                qPeopleCanLive_no,
            });
            quizHotel.Questions.Add(qPeopleCanLive);

            // 1
            var qSellFood = new Question
            {
                SystemName = "QUESTION_HOTEL_SELL_FOOD",
                Text = "QUESTION_HOTEL_SELL_FOOD",
                Icon = "~/img/SmartQuiz/7 food.svg",
            };
            var qSellFood_yes = new Choice
            {
                SystemName = "QUESTION_HOTEL_SELL_FOOD__YES",
                Text = "QUESTION_HOTEL_SELL_FOOD__YES",
            };
            var qSellFood_no = new Choice
            {
                SystemName = "QUESTION_HOTEL_SELL_FOOD__NO",
                Text = "QUESTION_HOTEL_SELL_FOOD__NO",
            };

            qSellFood.Choices.AddRange(new Choice[]
            {
                qSellFood_yes,
                qSellFood_no,
            });
            quizHotel.Questions.Add(qSellFood);

            // 1.1
            var qSellFoodArea = new Question
            {
                SystemName = "QUESTION_HOTEL_SELL_FOOD_AREA",
                Text = "QUESTION_HOTEL_SELL_FOOD_AREA",
            };
            var qSellFoodArea_LT200 = new Choice
            {
                SystemName = "QUESTION_HOTEL_SELL_FOOD_AREA__LT200",
                Text = "QUESTION_HOTEL_SELL_FOOD_AREA__LT200",
            };
            var qSellFoodArea_GT200 = new Choice
            {
                SystemName = "QUESTION_HOTEL_SELL_FOOD_AREA__GT200",
                Text = "QUESTION_HOTEL_SELL_FOOD_AREA__GT200",
            };

            qSellFoodArea.Choices.AddRange(new Choice[]
            {
                qSellFoodArea_LT200,
                qSellFoodArea_GT200,
            });
            qSellFood.SubQuestions.Add(qSellFoodArea);
            qSellFood_yes.NextQuestionID = qSellFoodArea.QuestionID.ToString();


            // 2
            var qSellAlcohol = new Question
            {
                SystemName = "QUESTION_HOTEL_SELL_ALCOHOL",
                Text = "QUESTION_HOTEL_SELL_ALCOHOL",
                Icon = "~/img/SmartQuiz/7 wine.svg",
            };
            var qSellAlcohol_yes = new Choice
            {
                SystemName = "QUESTION_HOTEL_SELL_ALCOHOL__YES",
                Text = "QUESTION_HOTEL_SELL_ALCOHOL__YES",
            };
            var qSellAlcohol_no = new Choice
            {
                SystemName = "QUESTION_HOTEL_SELL_ALCOHOL__NO",
                Text = "QUESTION_HOTEL_SELL_ALCOHOL__NO",
            };
            qSellAlcohol.Choices.AddRange(new Choice[]
            {
                qSellAlcohol_yes,
                qSellAlcohol_no,
            });
            quizHotel.Questions.Add(qSellAlcohol);


            var qStoreGas = new Question
            {
                SystemName = "QUESTION_HOTEL_STORE_GAS",
                Text = "QUESTION_HOTEL_STORE_GAS",
                Icon = "~/img/SmartQuiz/10 gas.svg",
            };
            var qStoreGas_no = new Choice
            {
                SystemName = "QUESTION_HOTEL_STORE_GAS__CHOICE_NO",
                Text = "QUESTION_HOTEL_STORE_GAS__CHOICE_NO"
            };
            var qStoreGas_le1000kg = new Choice
            {
                SystemName = "QUESTION_HOTEL_STORE_GAS__CHOICE_LE1000KG",
                Text = "QUESTION_HOTEL_STORE_GAS__CHOICE_LE1000KG"
            };
            var qStoreGas_gt1000kg = new Choice
            {
                SystemName = "QUESTION_HOTEL_STORE_GAS__CHOICE_GT1000KG",
                Text = "QUESTION_HOTEL_STORE_GAS__CHOICE_GT1000KG"
            };

            qStoreGas.Choices.Add(qStoreGas_no);
            qStoreGas.Choices.Add(qStoreGas_le1000kg);
            qStoreGas.Choices.Add(qStoreGas_gt1000kg);
            quizHotel.Questions.Add(qStoreGas);


            // 3
            var qSignTax = new Question
            {
                SystemName = "QUESTION_HOTEL_SIGN_TAX",
                Text = "QUESTION_HOTEL_SIGN_TAX",
            };
            var qSignTax_yes = new Choice
            {
                SystemName = "QUESTION_HOTEL_SIGN_TAX__YES",
                Text = "QUESTION_HOTEL_SIGN_TAX__YES",
            };
            var qSignTax_no = new Choice
            {
                SystemName = "QUESTION_HOTEL_SIGN_TAX__NO",
                Text = "QUESTION_HOTEL_SIGN_TAX__NO",
            };
            qSignTax.Choices.AddRange(new Choice[]
            {
                qSignTax_yes,
                qSignTax_no,
            });
            quizHotel.Questions.Add(qSignTax);

            // Add construction question
            AddShopConstructionQuestion(quizHotel, BUSINESS_HOTEL_TYPE);

            db.InsertOne(quizHotel);

            // ===========================================================
            // Quiz App Mapping Init
            // ===========================================================
            var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
            QuizAppMapping[] items = new QuizAppMapping[]
            {
                new QuizAppMapping()
                {
                     QuizGroup = BUSINESS_HOTEL_TYPE,
                     AppSystemName = "ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)",
                     DisplayConditions = new QuizAnswer[] {
                      new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
                      new QuizAnswer(qSellFoodArea.SystemName, qSellFoodArea_LT200.SystemName),
                },
                     OnlineRequestAllowedConditions = new QuizAnswer[]
                {
                     }
                    },
                    new QuizAppMapping()
                    {
                     QuizGroup = BUSINESS_HOTEL_TYPE,
                     AppSystemName = "ขอใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)",
                     DisplayConditions = new QuizAnswer[] {
                      new QuizAnswer(qSellFood.SystemName, qSellFood_yes.SystemName),
                      new QuizAnswer(qSellFoodArea.SystemName, qSellFoodArea_GT200.SystemName),
                     },
                     OnlineRequestAllowedConditions = new QuizAnswer[]
                     {
                     }
                    },
                    new QuizAppMapping()
                    {
                     QuizGroup = BUSINESS_HOTEL_TYPE,
                     AppSystemName = "ขอใบอนุญาตขายสุรา",
                     DisplayConditions = new QuizAnswer[] {
                      new QuizAnswer(qSellAlcohol.SystemName, qSellAlcohol_yes.SystemName),
                     },
                     OnlineRequestAllowedConditions = new QuizAnswer[]
                     {
                     }
                    },
                    new QuizAppMapping()
                    {
                     QuizGroup = BUSINESS_HOTEL_TYPE,
                     AppSystemName = "ยื่นชำระภาษีป้าย",
                     DisplayConditions = new QuizAnswer[] {
                      new QuizAnswer(qSignTax.SystemName, qSignTax_yes.SystemName),
                     },
                     OnlineRequestAllowedConditions = new QuizAnswer[]
                     {
                     }
                    },
                    new QuizAppMapping()
                    {
                     QuizGroup = BUSINESS_HOTEL_TYPE,
                                    //AppSystemName = "ขออนุญาตประกอบธุรกิจโรงแรม",
                                    AppSystemName = AppSystemNameTextConst.APP_HOTEL,
                     DisplayConditions = new QuizAnswer[] {
                      new QuizAnswer(qServiceMoreThanMonth.SystemName, qServiceMoreThanMonth_yes.SystemName),
                      new QuizAnswer(qRooms.SystemName, qRooms_yes.SystemName),
                      new QuizAnswer(qPeopleCanLive.SystemName, qPeopleCanLive_yes.SystemName)
                     },
                     OnlineRequestAllowedConditions = new QuizAnswer[]
                     {
                     }
            },

                new QuizAppMapping()
                {
                 QuizGroup = BUSINESS_HOTEL_TYPE,
                 AppSystemName = AppSystemNameTextConst.APP_STORE_GAS_LE1000KG,
                 DisplayConditions = new QuizAnswer[]
                 {
                  new QuizAnswer(qStoreGas.SystemName, qStoreGas_le1000kg.SystemName)
                 },
                 OnlineRequestAllowedConditions = new QuizAnswer[]
                 {
                 }
                },
                new QuizAppMapping()
                {
                 QuizGroup = BUSINESS_HOTEL_TYPE,
                 AppSystemName = AppSystemNameTextConst.APP_STORE_GAS_GT1000KG,
                 DisplayConditions = new QuizAnswer[]
                 {
                  new QuizAnswer(qStoreGas.SystemName, qStoreGas_gt1000kg.SystemName)
                 },
                 OnlineRequestAllowedConditions = new QuizAnswer[]
                 {
                 }
                },
                        };

            // Add construction app mapping
            items = AddShopConstructionAppMappings(items, BUSINESS_HOTEL_TYPE);

            dbAppMapping.InsertMany(items);
        }

        private static void InitSpaQuiz()
		{
			string BUSINESS_SPA_TYPE = BusinessGroupID.SPA;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizSpa = new SmartQuiz(BUSINESS_SPA_TYPE)
			{
				Text = "QUIZ_OPEN_SPA"
			};

			// 1
			var qProduct = new Question
			{
				SystemName = "QUESTION_SPA_PRODUCT",
				Text = "QUESTION_SPA_PRODUCT",
				Icon = "~/img/SmartQuiz/10 shop.svg",
				AllowMultipleAnswer = true
			};

			var qProduct_waterTreatment = new Choice
			{
				SystemName = "QUESTION_SPA_PRODUCT__WATER_TREATMENT",
				Text = "QUESTION_SPA_PRODUCT__WATER_TREATMENT",
			};

			var qProduct_massage = new Choice
			{
				SystemName = "QUESTION_SPA_PRODUCT__MASSAGE",
				Text = "QUESTION_SPA_PRODUCT__MASSAGE",
			};

			var qProduct_beauty = new Choice
			{
				SystemName = "QUESTION_SPA_PRODUCT__BEAUTY",
				Text = "QUESTION_SPA_PRODUCT__BEAUTY",
			};

			qProduct.Choices.AddRange(new Choice[]
			{
				qProduct_waterTreatment,
				qProduct_massage,
				qProduct_beauty,
			});
			quizSpa.Questions.Add(qProduct);

			// 2
			var qSignTax = new Question
			{
				SystemName = "QUESTION_SPA_SIGN_TAX",
				Text = "QUESTION_SPA_SIGN_TAX",
			};
			var qSignTax_yes = new Choice
			{
				SystemName = "QUESTION_SPA_SIGN_TAX__YES",
				Text = "QUESTION_SPA_SIGN_TAX__YES",
			};
			var qSignTax_no = new Choice
			{
				SystemName = "QUESTION_SPA_SIGN_TAX__NO",
				Text = "QUESTION_SPA_SIGN_TAX__NO",
			};
			qSignTax.Choices.AddRange(new Choice[]
			{
				qSignTax_yes,
				qSignTax_no,
			});
			quizSpa.Questions.Add(qSignTax);

			// Add construction question
			AddShopConstructionQuestion(quizSpa, BUSINESS_SPA_TYPE);

			db.InsertOne(quizSpa);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SPA_TYPE,
					AppSystemName = "ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_waterTreatment.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_massage.SystemName, "on"),
							new QuizAnswer(qProduct.SystemName + "_" + qProduct_beauty.SystemName, "on"),
						})
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SPA_TYPE,
					AppSystemName = "ยื่นชำระภาษีป้าย",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSignTax.SystemName, qSignTax_yes.SystemName),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},

			};

			// Add construction app mapping
			items = AddShopConstructionAppMappings(items, BUSINESS_SPA_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitTourismQuiz()
		{
			string BUSINESS_TOURISM_TYPE = BusinessGroupID.TOURISM;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizTourism = new SmartQuiz(BUSINESS_TOURISM_TYPE)
			{
				Text = "QUIZ_OPEN_TOURISM"
			};

			// 1
			var qTourist = new Question
			{
				SystemName = "QUESTION_TOURISM_TOURIST",
				Text = "QUESTION_TOURISM_TOURIST",
				//Icon = "~/img/SmartQuiz/10 shop.svg",
				Info = "QUESTION_TOURISM_TOURIST__INFO",

			};

			var qTourist_yes = new Choice
			{
				SystemName = "QUESTION_TOURISM_TOURIST_YES",
				Text = "QUESTION_TOURISM_TOURIST_YES",
			};
			var qTourist_no = new Choice
			{
				SystemName = "QUESTION_TOURISM_TOURIST_NO",
				Text = "QUESTION_TOURISM_TOURIST_NO",
			};

			qTourist.Choices.AddRange(new Choice[]
			{
				qTourist_yes,
				qTourist_no,
			});
			quizTourism.Questions.Add(qTourist);

			//1.1
			var qService = new Question
			{
				SystemName = "QUESTION_TOURISM_TOURIST_SERVICE",
				Text = "QUESTION_TOURISM_TOURIST_SERVICE",
				AllowMultipleAnswer = true,
			};
			var qService_All = new Choice
			{
				SystemName = "All",
				Text = "QUESTION_TOURISM_TOURIST_SERVICE_All",
				Info = "QUESTION_TOURISM_TOURIST_SERVICE_All__INFO"
			};
			var qService_Guide = new Choice
			{
				SystemName = "GUIDE",
				Text = "QUESTION_TOURISM_TOURIST_SERVICE_GUIDE",
			};
			var qService_Transport = new Choice
			{
				SystemName = "TRANSPORT",
				Text = "QUESTION_TOURISM_TOURIST_SERVICE_TRANSPORT",
			};

			var qService_Hotel = new Choice
			{
				SystemName = "HOTEL",
				Text = "QUESTION_TOURISM_TOURIST_SERVICE_HOTEL",
			};
			var qService_Food = new Choice
			{
				SystemName = "FOOD",
				Text = "QUESTION_TOURISM_TOURIST_SERVICE_FOOD",
			};
			var qService_Food_Hotel = new Choice
			{
				SystemName = "FOOD_HOTEL",
				Text = "QUESTION_TOURISM_TOURIST_SERVICE_FOOD_HOTEL",
			};

			qService.Choices.AddRange(new Choice[]
			{
				qService_All,
				qService_Guide,
				qService_Transport,
				qService_Hotel,
				qService_Food,
				qService_Food_Hotel
			});
			qTourist.SubQuestions.Add(qService);
			qTourist_yes.NextQuestionID = qService.QuestionID.ToString();
			
			//1.2 
			var qTour = new Question
			{
				SystemName = "QUESTION_TOURISM_TOURIST_TOUR",
				Text = "QUESTION_TOURISM_TOURIST_TOUR",
				AllowMultipleAnswer = true,
			};
			var qTour_Local = new Choice
			{
				SystemName = "LOCAL",
				Text = "QUESTION_TOURISM_TOURIST_TOUR_LOCAL",
			};
			var qTour_Domestic = new Choice
			{
				SystemName = "DOMESTIC",
				Text = "QUESTION_TOURISM_TOURIST_TOUR_DOMESTIC",
			};
			var qTour_Inbound = new Choice
			{
				SystemName = "INBOUND",
				Text = "QUESTION_TOURISM_TOURIST_TOUR_INBOUND",
			};
			var qTour_Outbound = new Choice
			{
				SystemName = "OUTBOUND",
				Text = "QUESTION_TOURISM_TOURIST_TOUR_OUTBOUND",

			};
			var qTour_No = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_TOURISM_TOURIST_TOUR_NO",

				ForbiddenChoices = new List<Guid>
				{
					qTour_Local.ChoiceID,
					qTour_Domestic.ChoiceID,
					qTour_Inbound.ChoiceID,
					qTour_Outbound.ChoiceID,

				}

			};
			qTour.Choices.AddRange(new Choice[]
			{
				qTour_Local,
				qTour_Domestic,
				qTour_Inbound,
				qTour_Outbound,
				qTour_No

			});
			qService.SubQuestions.Add(qTour);
			qTourist.SubQuestions.Add(qService);
			qService_All.NextQuestionID = qTour.QuestionID.ToString();
			qService_Guide.NextQuestionID = qTour.QuestionID.ToString();

			// Add sign-tax question
			AddSignTaxQuestion(quizTourism, BUSINESS_TOURISM_TYPE);

			#region Suggestions
			quizTourism.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามหรือค้นหาข้อมูลเพิ่มเติมได้ที่กรมการท่องเที่ยว <a href='https://www.dot.go.th/home'>https://www.dot.go.th/home</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qTourist.SystemName, new string[] { qTourist_no.SystemName }),
						new SuggestionCondition(qTour.SystemName + "_" + qTour_No.SystemName, new string[] { "true" })
					}
				},

				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามหรือค้นหาข้อมูลเพิ่มเติมได้ที่กรมการขนส่งทางบก <a href='https://www.dlt.go.th/th/'>https://www.dlt.go.th/th/</a>",
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qService.SystemName + "_" + qService_Transport.SystemName, new string[] { "true" })
					}
				},

				new Suggestion()
				{
					Message = "กรุณาดู Smart Quiz เพิ่มเติมเกี่ยวกับธุรกิจรีสอร์ทขนาดเล็ก/โรงแรม",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qService.SystemName + "_" + qService_Hotel.SystemName, new string[] { "true" }),
						new SuggestionCondition(qService.SystemName + "_" + qService_Food_Hotel.SystemName, new string[] { "true" })
					}
				},

				new Suggestion()
				{
					Message = "กรุณาดู Smart Quiz เพิ่มเติมเกี่ยวกับธุรกิจร้านอาหารและเครื่องดื่ม",
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qService.SystemName + "_" + qService_Food.SystemName, new string[] { "true" })
					}
				}
			};

			#endregion

			db.InsertOne(quizTourism);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_TOURISM_TYPE,
					AppSystemName =  AppSystemNameTextConst.APP_TOURISM_BUSINESS,

					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						new QuizAnswer(qTour.SystemName + "_" + qTour_Local.SystemName, "on"),
						new QuizAnswer(qTour.SystemName + "_" + qTour_Domestic.SystemName, "on"),
						new QuizAnswer(qTour.SystemName + "_" + qTour_Inbound.SystemName, "on"),
						new QuizAnswer(qTour.SystemName + "_" + qTour_Outbound.SystemName, "on"),
						})
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				}
			};
			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_TOURISM_TYPE);
			dbAppMapping.InsertMany(items);
		}

		private static void InitLegalQuiz()
		{
			string BUSINESS_LEGAL_TYPE = BusinessGroupID.LEGAL;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizLegal = new SmartQuiz(BUSINESS_LEGAL_TYPE)
			{
				Text = "QUIZ_OPEN_LEGAL"
			};
			// 1
			var qLegalCounsel = new Question
			{
				SystemName = "QUESTION_LEGAL_COUNSEL",
				Text = "QUESTION_LEGAL_COUNSEL",

			};

			var qLegalCounsel_yes = new Choice
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_YES",
				Text = "QUESTION_LEGAL_COUNSEL_YES",
			};
			var qLegalCounsel_no = new Choice
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_NO",
				Text = "QUESTION_LEGAL_COUNSEL_NO",
			};

			qLegalCounsel.Choices.AddRange(new Choice[]
			{
				qLegalCounsel_yes,
				qLegalCounsel_no,
			});
			quizLegal.Questions.Add(qLegalCounsel);

			//1.1
			var qRegisterOffice = new Question
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE",
				Text = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE",

				//AllowMultipleAnswer = true

			};
			var qRegisterOffice_yes = new Choice
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_YES",
				Text = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_YES",
				Info = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_YES__INFO",
			};
			var qRegisterOffice_no = new Choice
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_NO",
				Text = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_NO",
				//Info = "QUESTION_LEGAL_COUNSEL_NO__INFO",
			};
			qRegisterOffice.Choices.AddRange(new Choice[]
			{
				qRegisterOffice_yes,
				qRegisterOffice_no
			});
			qLegalCounsel.SubQuestions.Add(qRegisterOffice);
			qLegalCounsel_yes.NextQuestionID = qRegisterOffice.QuestionID.ToString();

			//1.1.1

			var qHeadLawyer = new Question
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_HEAD",
				Text = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_HEAD",

			};
			var qHeadLawyer_yes = new Choice
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_HEAD_YES",
				Text = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_HEAD_YES",
				Info = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_HEAD_YES_INFO"
			};
			var qHeadLawyer_no = new Choice
			{
				SystemName = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_HEAD_NO",
				Text = "QUESTION_LEGAL_COUNSEL_REGISTER_OFFICE_HEAD_NO",

			};
			qHeadLawyer.Choices.AddRange(new Choice[]
			{
				qHeadLawyer_yes,
				qHeadLawyer_no
			});
			qRegisterOffice.SubQuestions.Add(qHeadLawyer);
			qRegisterOffice_no.NextQuestionID = qHeadLawyer.QuestionID.ToString();
			//1.1.1.1
			var qLawFirmName = new Question
			{
				SystemName = "QUESTION_LEGAL_LAW_FIRM_NAME",
				Text = "QUESTION_LEGAL_LAW_FIRM_NAME",
                Info = "QUESTION_LEGAL_LAW_FIRM_NAME__INFO",

            };
			var qLawFirmName_yes = new Choice
			{
				SystemName = "QUESTION_LEGAL_LAW_FIRM_NAME_YES",
				Text = "QUESTION_LEGAL_LAW_FIRM_NAME_YES",

			};
			var qLawFirmName_no = new Choice
			{
				SystemName = "QUESTION_LEGAL_LAW_FIRM_NAME_NO",
				Text = "QUESTION_LEGAL_LAW_FIRM_NAME_NO",

			};
			qLawFirmName.Choices.AddRange(new Choice[]
			{
				qLawFirmName_yes,
				qLawFirmName_no
			});
			qHeadLawyer.SubQuestions.Add(qLawFirmName);
			qHeadLawyer_yes.NextQuestionID = qLawFirmName.QuestionID.ToString();

			// 2
			var qAccounting = new Question
			{
				SystemName = "QUESTION_ACCOUNTING",
				Text = "QUESTION_ACCOUNTING",

			};
			var qAccounting_yes = new Choice
			{
				SystemName = "QUESTION_ACCOUNTING_YES",
				Text = "QUESTION_ACCOUNTING_YES",
			};
			var qAccounting_no = new Choice
			{
				SystemName = "QUESTION_ACCOUNTING_NO",
				Text = "QUESTION_ACCOUNTING_NO",
			};
			qAccounting.Choices.AddRange(new Choice[]
			{
				qAccounting_yes,
				qAccounting_no
			});

            quizLegal.Questions.Add(qAccounting);

            // 2.1
            var qAccountingType = new Question
			{
				SystemName = "QUESTION_ACCOUNTING_TYPE",
				Text = "QUESTION_ACCOUNTING_TYPE",

			};
			var qAccountingType_Individual = new Choice
			{
				SystemName = "QUESTION_ACCOUNTING_TYPE_INDIVIDUAL",
				Text = "QUESTION_ACCOUNTING_TYPE_INDIVIDUAL",
			};
			var qAccountingType_Juristic = new Choice
			{
				SystemName = "QUESTION_ACCOUNTING_TYPE_JURISTIC",
				Text = "QUESTION_ACCOUNTING_TYPE_JURISTIC",
			};
			qAccountingType.Choices.AddRange(new Choice[]
			{
				qAccountingType_Individual,
				qAccountingType_Juristic
			});
			qAccounting.SubQuestions.Add(qAccountingType);
			qAccounting_yes.NextQuestionID = qAccountingType.QuestionID.ToString();
			// 2.1.1
			var qAccountingObjective = new Question
			{
				SystemName = "QUESTION_ACCOUNTING_OBJECTIVE",
				Text = "QUESTION_ACCOUNTING_OBJECTIVE",

			};
			var qAccountingObjective_yes = new Choice
			{
				SystemName = "QUESTION_ACCOUNTING_OBJECTIVE_YES",
				Text = "QUESTION_ACCOUNTING_OBJECTIVE_YES",
			};
			var qAccountingObjective_no = new Choice
			{
				SystemName = "QUESTION_ACCOUNTING_OBJECTIVE_NO",
				Text = "QUESTION_ACCOUNTING_OBJECTIVE_NO",
			};
			qAccountingObjective.Choices.AddRange(new Choice[]
			{
				qAccountingObjective_yes,
				qAccountingObjective_no
			});
			qAccountingType.SubQuestions.Add(qAccountingObjective);
			qAccountingType_Juristic.NextQuestionID = qAccountingObjective.QuestionID.ToString();
			// Add sign-tax question
			AddSignTaxQuestion(quizLegal, BUSINESS_LEGAL_TYPE);

			#region Suggestions
			quizLegal.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามข้อมูลเพิ่มเติมได้ที่สภาทนายความ ในพระบรมราชูปถัมป์  <a href='https://www.lawyerscouncil.or.th/2019/en/homepage/'>https://www.lawyerscouncil.or.th/2019/en/homepage/</a> หรือติดต่อเบอร์ 02 522 7124",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qRegisterOffice.SystemName, new string[] { qRegisterOffice_yes.SystemName }),
						new SuggestionCondition(qHeadLawyer.SystemName, new string[] { qHeadLawyer_no.SystemName }),
						new SuggestionCondition(qLawFirmName.SystemName, new string[] { qLawFirmName_no.SystemName }),
					}
				},

				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามข้อมูลเพิ่มเติมได้ที่กรมพัฒนาธุรกิจการค้า <a href='https://www.dbd.go.th/index.php'>https://www.dbd.go.th/index.php</a>",
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qAccountingObjective.SystemName, new string[] { qAccountingObjective_no.SystemName }),

					}
				},

                /*new Suggestion()
                {
                    Message = "คุณเข้าข่ายต้องขอหนังสือทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี หากท่านต้องการขอ<br>หนังสือทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพ สามารถติดต่อได้ที่ <a href='http://www.tfac.or.th/Home/Main'>http://www.tfac.or.th/Home/Main</a> <br>หรือ ติดต่ออาคารสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์ เลขที่ 133 ถนนสุขุมวิท 21 (อโศก) แขวงคลองเตยเหนือ เขตวัฒนา <br>กรุงเทพฯ 1011 หรือ โทร 0-2685-2500",
                    RequireAllCondition = false,
                    Conditions = new SuggestionCondition[]
                    {
                        new SuggestionCondition(qAccountingObjective.SystemName, new string[] { qAccountingObjective_yes.SystemName }),
                    }
                },*/

                new Suggestion()
				{
					Message = "ไม่เข้าเงื่อนไขที่ต้องจดทะเบียนกับสภาวิชาชีพบัญชี ในพระบรมราชูปถัมภ์",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qAccounting.SystemName, new string[] { qAccounting_no.SystemName }),
						new SuggestionCondition(qAccountingType.SystemName, new string[] { qAccountingType_Individual.SystemName }),
					}
				},


			};

			#endregion

			db.InsertOne(quizLegal);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_LEGAL_TYPE,
					AppSystemName =  AppSystemNameTextConst.APP_LAW_OFFICE,


					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qLawFirmName.SystemName, qLawFirmName_yes.SystemName)
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
                
                new QuizAppMapping()
				{
					QuizGroup = BUSINESS_LEGAL_TYPE,
					AppSystemName =  AppSystemNameTextConst.APP_ACCOUNTING_SERVICE,


					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qAccountingObjective.SystemName, qAccountingObjective_yes.SystemName)
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				}

			};

			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_LEGAL_TYPE);

			dbAppMapping.InsertMany(items);
		}

		private static void InitSoftwareQuiz()
		{
			string BUSINESS_SOFTWARE_TYPE = BusinessGroupID.SOFTWARE;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizSoftware = new SmartQuiz(BUSINESS_SOFTWARE_TYPE)
			{
				Text = "QUIZ_OPEN_SOFTWARE"
			};

			// 1
			var qRegisterVAT = new Question
			{
				SystemName = "QUESTION_REGISTER_VAT",
				Text = "QUESTION_REGISTER_VAT",

			};
			var qRegisterVAT_yes = new Choice
			{
				SystemName = "QUESTION_REGISTER_VAT_YES",
				Text = "QUESTION_REGISTER_VAT_YES",
			};
			var qRegisterVAT_no = new Choice
			{
				SystemName = "QUESTION_REGISTER_VAT_NO",
				Text = "QUESTION_REGISTER_VAT_NO",
			};
			qRegisterVAT.Choices.AddRange(new Choice[]
			{
				qRegisterVAT_yes,
				qRegisterVAT_no,
			});
			quizSoftware.Questions.Add(qRegisterVAT);
			//1.1
			var qSoftwareType = new Question
			{
				SystemName = "QUESTION_SOFTWARE_TYPE",
				Text = "QUESTION_SOFTWARE_TYPE",
				AllowMultipleAnswer = true,
			};
			var qSoftwareType_InHouse = new Choice
			{
				SystemName = "INHOUSE",
				Text = "QUESTION_SOFTWARE_TYPE_INHOUSE",

			};
			var qSoftwareType_LocalInternational = new Choice
			{
				SystemName = "LOCAL_INTERNATIONAL",
				Text = "QUESTION_SOFTWARE_TYPE_LOCAL_INTERNATIONAL",

			};
			var qSoftwareType_Dealer = new Choice
			{
				SystemName = "DEALER",
				Text = "QUESTION_SOFTWARE_TYPE_DEALER",

			};
			var qSoftware_DevelopImport = new Choice
			{
				SystemName = "DEVELOP_IMPORT",
				Text = "QUESTION_SOFTWARE_TYPE_DEVELOP_IMPORT",

			};
			var qSoftwareType_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_SOFTWARE_TYPE_NO",
				ForbiddenChoices = new List<Guid>
				{
					qSoftwareType_InHouse.ChoiceID,
					qSoftwareType_LocalInternational.ChoiceID,
					qSoftwareType_Dealer.ChoiceID,
					qSoftware_DevelopImport.ChoiceID,

				},

			};
			qSoftwareType.Choices.AddRange(new Choice[]
			{
				qSoftwareType_InHouse,
				qSoftwareType_LocalInternational,
				qSoftwareType_Dealer,
				qSoftware_DevelopImport,
				qSoftwareType_no

			});
			qRegisterVAT.SubQuestions.Add(qSoftwareType);
			qRegisterVAT_yes.NextQuestionID = qSoftwareType.QuestionID.ToString();

			//1.1.1
			var qSoftwareSpec = new Question
			{
				SystemName = "QUESTION_SOFTWARE_SPEC",
				Text = "QUESTION_SOFTWARE_SPEC",

			};
			var qSoftwareSpec_yes = new Choice
			{
				SystemName = "QUESTION_SOFTWARE_SPEC_YES",
				Text = "QUESTION_SOFTWARE_SPEC_YES",

			};
			var qSoftwareSpec_no = new Choice
			{
				SystemName = "QUESTION_SOFTWARE_SPEC_NO",
				Text = "QUESTION_SOFTWARE_SPEC_NO",

			};
			qSoftwareSpec.Choices.AddRange(new Choice[]
			{
				qSoftwareSpec_yes,
				qSoftwareSpec_no,
			});

			qSoftwareType_InHouse.NextQuestionID = qSoftwareSpec.QuestionID.ToString();
			qSoftwareType_LocalInternational.NextQuestionID = qSoftwareSpec.QuestionID.ToString();
			qSoftwareType_Dealer.NextQuestionID = qSoftwareSpec.QuestionID.ToString();
			qSoftware_DevelopImport.NextQuestionID = qSoftwareSpec.QuestionID.ToString();
			qSoftwareType_no.NextQuestionID = "";
			qSoftwareType.SubQuestions.Add(qSoftwareSpec);
			//1.1.1.1
			var qSoftwareSpec_Additional = new Question
			{
				SystemName = "QUESTION_SOFTWARE_SPEC_ADDITIONAL",
				Text = "QUESTION_SOFTWARE_SPEC_ADDITIONAL",
				AllowMultipleAnswer = true,
			};
			var qSoftwareSpec_Additional_SomeComputer = new Choice
			{
				SystemName = "SOME",
				Text = "QUESTION_SOFTWARE_SPEC_ADDITIONAL_SOME",

			};
			var qSoftwareSpec_Additional_AllComputer = new Choice
			{
				SystemName = "All",
				Text = "QUESTION_SOFTWARE_SPEC_ADDITIONAL_All",

			};
			var qSoftwareSpec_Additional_Security = new Choice
			{
				SystemName = "SECURITY",
				Text = "QUESTION_SOFTWARE_SPEC_ADDITIONAL_SECURITY",

			};
			var qSoftwareSpec_Additional_No = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_SOFTWARE_SPEC_ADDITIONAL_NO",

			};

			qSoftwareSpec_Additional.Choices.AddRange(new Choice[]
			{
				qSoftwareSpec_Additional_SomeComputer,
				qSoftwareSpec_Additional_AllComputer,
				qSoftwareSpec_Additional_Security,
				qSoftwareSpec_Additional_No
			});
			qSoftwareSpec.SubQuestions.Add(qSoftwareSpec_Additional);
			qSoftwareSpec_yes.NextQuestionID = qSoftwareSpec_Additional.QuestionID.ToString();


			#region Suggestions
			quizSoftware.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามข้อมูลเพิ่มเติมได้ที่กรมสรรพากร <a href='https://www.rd.go.th'>www.rd.go.th</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{

						new SuggestionCondition(qRegisterVAT.SystemName, new string[] { qRegisterVAT_no.SystemName }),
						new SuggestionCondition(qSoftwareSpec.SystemName, new string[] { qSoftwareSpec_no.SystemName }),
						new SuggestionCondition(qSoftwareType.SystemName+"_"+qSoftwareType_no.SystemName, new string[] { "true" }),

					}
				},




			};

			#endregion

			db.InsertOne(quizSoftware);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SOFTWARE_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_SOFTWARE_HOUSE_NEW,  //"ขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์ ชนิด ก",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true,new QuizAnswer[]{
						new QuizAnswer(qSoftwareSpec_Additional.SystemName + "_" + qSoftwareSpec_Additional_No.SystemName, "on"),
						new QuizAnswer(qSoftwareSpec_Additional.SystemName + "_" + qSoftwareSpec_Additional_SomeComputer.SystemName, "on"),
						new QuizAnswer(qSoftwareSpec_Additional.SystemName + "_" + qSoftwareSpec_Additional_AllComputer.SystemName, "on"),
						new QuizAnswer(qSoftwareSpec_Additional.SystemName + "_" + qSoftwareSpec_Additional_Security.SystemName, "on"),
						})
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				//new QuizAppMapping()
				//{
				//	QuizGroup = BUSINESS_SOFTWARE_TYPE,
				//	AppSystemName =  "ขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์ ชนิด ข",


				//	DisplayConditions = new QuizAnswer[] {
				//		new QuizAnswer(qSoftwareSpec_Additional.SystemName + "_" + qSoftwareSpec_Additional_SomeComputer.SystemName, "on"),
				//	},

				//	OnlineRequestAllowedConditions = new QuizAnswer[]
				//	{
				//	}
				//},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SOFTWARE_TYPE,
					AppSystemName =  "ขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์ ชนิด ค",


					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSoftwareSpec_Additional.SystemName + "_" + qSoftwareSpec_Additional_AllComputer.SystemName, "on"),
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SOFTWARE_TYPE,
					AppSystemName =  "ขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์ ชนิด ง",


					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qSoftwareSpec_Additional.SystemName + "_" + qSoftwareSpec_Additional_Security.SystemName, "on"),
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},

			};



			dbAppMapping.InsertMany(items);
		}
		private static void InitLogisticsQuiz()
		{
			string BUSINESS_LOGISTICS_TYPE = BusinessGroupID.LOGISTICS;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizLogistics = new SmartQuiz(BUSINESS_LOGISTICS_TYPE)
			{
				Text = "QUIZ_OPEN_LOGISTICS"
			};

			// 1
			var qLogistics = new Question
			{
				SystemName = "QUESTION_LOGISTICS",
				Text = "QUESTION_LOGISTICS",

			};

			var qLogistics_yes = new Choice
			{
				SystemName = "QUESTION_LOGISTICS_YES",
				Text = "QUESTION_LOGISTICS_YES",
			};
			var qLogistics_no = new Choice
			{
				SystemName = "QUESTION_LOGISTICS_NO",
				Text = "QUESTION_LOGISTICS_NO",
			};

			qLogistics.Choices.AddRange(new Choice[]
			{
				qLogistics_yes,
				qLogistics_no,
			});
			quizLogistics.Questions.Add(qLogistics);

			//1.1
			var qLogisticsType = new Question
			{
				SystemName = "QUESTION_LOGISTICS_TYPE",
				Text = "QUESTION_LOGISTICS_TYPE",


			};
			var qLogisticsType_Hire = new Choice
			{
				SystemName = "QUESTION_LOGISTICS_TYPE_HIRE",
				Text = "QUESTION_LOGISTICS_TYPE_HIRE",

			};
			var qLogisticsType_Owner = new Choice
			{
				SystemName = "QUESTION_LOGISTICS_TYPE_OWNER",
				Text = "QUESTION_LOGISTICS_TYPE_OWNER",

			};


			qLogisticsType.Choices.AddRange(new Choice[]
			{
				qLogisticsType_Hire,
				qLogisticsType_Owner
			});
			qLogistics.SubQuestions.Add(qLogisticsType);
			qLogistics_yes.NextQuestionID = qLogisticsType.QuestionID.ToString();

			// Add sign-tax question
			AddSignTaxQuestion(quizLogistics, BUSINESS_LOGISTICS_TYPE);

			#region Suggestions
			quizLogistics.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามเพิ่มเติมได้ที่กรมการขนส่งทางบก <a href='https://www.dlt.go.th/th/'>https://www.dlt.go.th/th/</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qLogistics.SystemName, new string[] { qLogistics_no.SystemName }),
						new SuggestionCondition(qLogisticsType.SystemName, new string[] { qLogisticsType_Owner.SystemName }),

					}
				},
			};
			#endregion

			db.InsertOne(quizLogistics);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_LOGISTICS_TYPE,
					AppSystemName =  AppSystemNameTextConst.APP_TRANSPORT_NON_REGULAR_TRUCK,


					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qLogisticsType.SystemName, qLogisticsType_Hire.SystemName)
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},


			};

			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_LOGISTICS_TYPE);

			dbAppMapping.InsertMany(items);
		}
		private static void InitEducationQuiz()
		{
			string BUSINESS_EDUCATION_TYPE = BusinessGroupID.EDUCATION;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizEducation = new SmartQuiz(BUSINESS_EDUCATION_TYPE)
			{
				Text = "QUIZ_OPEN_EDUCATION"
			};

			// 1
			var qTeachingType = new Question
			{
				SystemName = "QUESTION_EDUCATION_TYPE",
				Text = "QUESTION_EDUCATION_TYPE",
				AllowMultipleAnswer = true,

			};

			var qTeachingType_Art_Sport = new Choice
			{
				SystemName = "ART_SPORT",
				Text = "QUESTION_EDUCATION_ART_SPORT",
			};
			var qTeachingType_Professional = new Choice
			{
				SystemName = "PROFESSIONAL",
				Text = "QUESTION_EDUCATION_TYPE_PROFESSIONAL",
			};
			var qTeachingType_Tutor = new Choice
			{
				SystemName = "TUTOR",
				Text = "QUESTION_EDUCATION_TYPE_TUTOR",
			};
			var qTeachingType_Life_Skills = new Choice
			{
				SystemName = "LIFE_SKILLS",
				Text = "QUESTION_EDUCATION_TYPE_LIFE_SKILLS",
			};
			var qTeachingType_Religion = new Choice
			{
				SystemName = "RELIGION",
				Text = "QUESTION_EDUCATION_TYPE_RELIGION",
			};
			var qTeachingType_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_EDUCATION_TYPE_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qTeachingType_Art_Sport.ChoiceID,
					qTeachingType_Professional.ChoiceID,
					qTeachingType_Tutor.ChoiceID,
					qTeachingType_Life_Skills.ChoiceID,
					qTeachingType_Religion.ChoiceID,

				}
			};
			qTeachingType.Choices.AddRange(new Choice[]
			{
				qTeachingType_Art_Sport,
				qTeachingType_Professional,
				qTeachingType_Tutor,
				qTeachingType_Life_Skills,
				qTeachingType_Religion,
				qTeachingType_no
			});
			quizEducation.Questions.Add(qTeachingType);

			//1.1
			var qSchool_Location = new Question
			{
				SystemName = "QUESTION_EDUCATION_LOCATION",
				Text = "QUESTION_EDUCATION_LOCATION",

			};
			var qSchool_Location_yes = new Choice
			{
				SystemName = "QUESTION_EDUCATION_LOCATION_YES",
				Text = "QUESTION_EDUCATION_LOCATION_YES",

			};
			var qSchool_Location_no = new Choice
			{
				SystemName = "QUESTION_EDUCATION_LOCATION_NO",
				Text = "QUESTION_EDUCATION_LOCATION_NO",

			};

			qSchool_Location.Choices.AddRange(new Choice[]
			{
				qSchool_Location_yes,
				qSchool_Location_no,
			});
			qTeachingType.SubQuestions.Add(qSchool_Location);
			qTeachingType_Art_Sport.NextQuestionID = qSchool_Location.QuestionID.ToString();
			qTeachingType_Professional.NextQuestionID = qSchool_Location.QuestionID.ToString();
			qTeachingType_Tutor.NextQuestionID = qSchool_Location.QuestionID.ToString();
			qTeachingType_Life_Skills.NextQuestionID = qSchool_Location.QuestionID.ToString();
			qTeachingType_Religion.NextQuestionID = qSchool_Location.QuestionID.ToString();

			////1.1.1

			var qSchool_Location_Type = new Question
			{
				SystemName = "QUESTION_EDUCATION_LOCATION_TYPE",
				Text = "QUESTION_EDUCATION_LOCATION_TYPE",
				AllowMultipleAnswer = true,

			};
			var qSchool_Location_Type_Shopping_Center = new Choice
			{
				SystemName = "SHOPPING_CENTER",
				Text = "QUESTION_EDUCATION_LOCATION_TYPE_SHOPPING_CENTER",

			};
			var qSchool_Location_Type_Com_Building = new Choice
			{
				SystemName = "COM_BUILDING",
				Text = "QUESTION_EDUCATION_LOCATION_TYPE_COM_BUILDING",

			};
			var qSchool_Location_Type_Office_Building = new Choice
			{
				SystemName = "OFFICE_BUILDING",
				Text = "QUESTION_EDUCATION_LOCATION_TYPE_OFFICE_BUILDING",

			};
			var qSchool_Location_Type_Town_Home = new Choice
			{
				SystemName = "TOWN_HOME",
				Text = "QUESTION_EDUCATION_LOCATION_TYPE_TOWN_HOME",

			};
			var qSchool_Location_Type_Home = new Choice
			{
				SystemName = "HOME",
				Text = "QUESTION_EDUCATION_LOCATION_TYPE_HOME",

			};
			var qSchool_Location_Type_Condo = new Choice
			{
				SystemName = "CONDO",
				Text = "QUESTION_EDUCATION_LOCATION_TYPE_CONDO",
				ForbiddenChoices = new List<Guid>()
				{
					qSchool_Location_Type_Shopping_Center.ChoiceID,
					qSchool_Location_Type_Com_Building.ChoiceID,
					qSchool_Location_Type_Office_Building.ChoiceID,
					qSchool_Location_Type_Town_Home.ChoiceID,
					qSchool_Location_Type_Home.ChoiceID,

				}

			};
			qSchool_Location_Type.Choices.AddRange(new Choice[]
			{
				qSchool_Location_Type_Shopping_Center,
				qSchool_Location_Type_Com_Building,
				qSchool_Location_Type_Office_Building,
				qSchool_Location_Type_Town_Home,
				qSchool_Location_Type_Home,
				qSchool_Location_Type_Condo
			});
			qSchool_Location_yes.NextQuestionID = qSchool_Location_Type.QuestionID.ToString();
			qSchool_Location.SubQuestions.Add(qSchool_Location_Type);
			//quizEducation.Questions.Add(qSchool_Location);


			////1.1.1.1
			var qConstruction_Cert = new Question
			{
				SystemName = "QUESTION_EDUCATION_CONTRUCTION_CERT",
				Text = "QUESTION_EDUCATION_CONTRUCTION_CERT",

			};
			var qConstruction_Cert_yes = new Choice
			{
				SystemName = "QUESTION_EDUCATION_CONTRUCTION_CERT_YES",
				Text = "QUESTION_EDUCATION_CONTRUCTION_CERT_YES",

			};
			var qConstruction_Cert_no = new Choice
			{
				SystemName = "QUESTION_EDUCATION_CONTRUCTION_CERT_NO",
				Text = "QUESTION_EDUCATION_CONTRUCTION_CERT_NO",

			};
			qConstruction_Cert.Choices.AddRange(new Choice[]
			{
				qConstruction_Cert_yes,
				qConstruction_Cert_no,
			});
			qSchool_Location_Type.SubQuestions.Add(qConstruction_Cert);
			qSchool_Location_Type_Shopping_Center.NextQuestionID = qConstruction_Cert.QuestionID.ToString();
			qSchool_Location_Type_Com_Building.NextQuestionID = qConstruction_Cert.QuestionID.ToString();
			qSchool_Location_Type_Office_Building.NextQuestionID = qConstruction_Cert.QuestionID.ToString();
			qSchool_Location_Type_Town_Home.NextQuestionID = qConstruction_Cert.QuestionID.ToString();
			qSchool_Location_Type_Home.NextQuestionID = qConstruction_Cert.QuestionID.ToString();
			//quizEducation.Questions.Add(qSchool_Location_Type);
			////1.1.2

			var qCourses = new Question
			{
				SystemName = "QUESTION_EDUCATION_COURSES",
				Text = "QUESTION_EDUCATION_COURSES",

			};
			var qCourses_Less600 = new Choice
			{
				SystemName = "QUESTION_EDUCATION_COURSES_LESS600",
				Text = "QUESTION_EDUCATION_COURSES_LESS600",

			};
			var qCourses_More600 = new Choice
			{
				SystemName = "QUESTION_EDUCATION_COURSES_MORE600",
				Text = "QUESTION_EDUCATION_COURSES_MORE600",

			};
			qCourses.Choices.AddRange(new Choice[]
			{
				qCourses_Less600,
				qCourses_More600,
			});
			qConstruction_Cert.SubQuestions.Add(qCourses);
			qConstruction_Cert_yes.NextQuestionID = qCourses.QuestionID.ToString();

			////1.1.2.1
			var qSpace = new Question
			{
				SystemName = "QUESTION_EDUCATION_SPACE",
				Text = "QUESTION_EDUCATION_SPACE",

			};
			var qSpace_Less100 = new Choice
			{
				SystemName = "QUESTION_EDUCATION_SPACE_LESS100",
				Text = "QUESTION_EDUCATION_SPACE_LESS100",

			};
			var qSpace_More100 = new Choice
			{
				SystemName = "QUESTION_EDUCATION_SPACE_MORE100",
				Text = "QUESTION_EDUCATION_SPACE_MORE100",

			};
			qSpace.Choices.AddRange(new Choice[]
			{
				qSpace_Less100,
				qSpace_More100,
			});
			qCourses.SubQuestions.Add(qSpace);
			qCourses_Less600.NextQuestionID = qSpace.QuestionID.ToString();
			//quizEducation.Questions.Add(qCourses);

			////1.1.2.2
			var qSpace2 = new Question
			{
				SystemName = "QUESTION_EDUCATION_SPACE2",
				Text = "QUESTION_EDUCATION_SPACE",

			};
			var qSpace2_Less200 = new Choice
			{
				SystemName = "QUESTION_EDUCATION_SPACE2_LESS200",
				Text = "QUESTION_EDUCATION_SPACE_LESS200",

			};
			var qSpace2_More200 = new Choice
			{
				SystemName = "QUESTION_EDUCATION_SPACE2_MORE200",
				Text = "QUESTION_EDUCATION_SPACE_MORE200",

			};
			qSpace2.Choices.AddRange(new Choice[]
			{
				qSpace2_Less200,
				qSpace2_More200,
			});
			qCourses.SubQuestions.Add(qSpace2);
			qCourses_More600.NextQuestionID = qSpace2.QuestionID.ToString();
			////1.1.3
			var qFounderType = new Question
			{
				SystemName = "QUESTION_EDUCATION_FOUNDER_TYPE",
				Text = "QUESTION_EDUCATION_FOUNDER_TYPE",

			};
			var qFounderType_Individual = new Choice
			{
				SystemName = "QUESTION_EDUCATION_FOUNDER_TYPE_INDIVIDUAL",
				Text = "QUESTION_EDUCATION_FOUNDER_TYPE_INDIVIDUAL",

			};
			var qFounderType_Juristic = new Choice
			{
				SystemName = "QUESTION_EDUCATION_FOUNDER_TYPE_JURISTIC",
				Text = "QUESTION_EDUCATION_FOUNDER_TYPE_JURISTIC",

			};
			qFounderType.Choices.AddRange(new Choice[]
			{
				qFounderType_Individual,
				qFounderType_Juristic,
			});
			
			
			qSpace_More100.NextQuestionID = qFounderType.QuestionID.ToString();
			qSpace.SubQuestions.Add(qFounderType);
			//quizEducation.Questions.Add(qFounderType);
		
	
			
			

			////1.1.3.1
			var qFounder_Qualified_Individual = new Question
			{
				SystemName = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL",
				Text = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL",
				Info = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_INFO"

			};
			var qFounder_Qualified_Individual_yes = new Choice
			{
				SystemName = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_YES",
				Text = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_YES",

			};
			var qFounder_Qualified_Individual_no = new Choice
			{
				SystemName = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_NO",
				Text = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_NO",

			};
			qFounder_Qualified_Individual.Choices.AddRange(new Choice[]
			{
				qFounder_Qualified_Individual_yes,
				qFounder_Qualified_Individual_no,
			});

			//qSpace2_More200.NextQuestionID = qFounderType.QuestionID.ToString();
			qSpace2.SubQuestions.Add(qFounderType);

			qFounderType.SubQuestions.Add(qFounder_Qualified_Individual);
			qFounderType_Individual.NextQuestionID = qFounder_Qualified_Individual.QuestionID.ToString();

			////1.1.3.2
			var qFounder_Qualified_Juristic = new Question
			{
				SystemName = "QUESTION_EDUCATION_QUALIFIED_JURISTIC",
				Text = "QUESTION_EDUCATION_QUALIFIED_JURISTIC",
				Info = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_INFO"

			};
			var qFounder_Qualified_Juristic_yes = new Choice
			{
				SystemName = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_YES",
				Text = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_YES",

			};
			var qFounder_Qualified_Juristic_no = new Choice
			{
				SystemName = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_NO",
				Text = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_NO",

			};
			qFounder_Qualified_Juristic.Choices.AddRange(new Choice[]
			{
				qFounder_Qualified_Juristic_yes,
				qFounder_Qualified_Juristic_no,
			});
			qFounderType.SubQuestions.Add(qFounder_Qualified_Juristic);
			qFounderType_Juristic.NextQuestionID = qFounder_Qualified_Juristic.QuestionID.ToString();


            // Use separated set of founder qualification otherwise they will be rendered as incorrect order.

            ////1.1.3/2
            var qFounderType2 = new Question
            {
                SystemName = "QUESTION_EDUCATION_FOUNDER_TYPE2",
                Text = "QUESTION_EDUCATION_FOUNDER_TYPE",

            };
            var qFounderType2_Individual = new Choice
            {
                SystemName = "QUESTION_EDUCATION_FOUNDER_TYPE2_INDIVIDUAL",
                Text = "QUESTION_EDUCATION_FOUNDER_TYPE_INDIVIDUAL",

            };
            var qFounderType2_Juristic = new Choice
            {
                SystemName = "QUESTION_EDUCATION_FOUNDER_TYPE2_JURISTIC",
                Text = "QUESTION_EDUCATION_FOUNDER_TYPE_JURISTIC",

            };
            qFounderType2.Choices.AddRange(new Choice[]
            {
                qFounderType2_Individual,
                qFounderType2_Juristic,
            });

            qSpace2_More200.NextQuestionID = qFounderType2.QuestionID.ToString();
            qSpace2.SubQuestions.Add(qFounderType2);

            ////1.1.3.1/2
            var qFounder2_Qualified_Individual = new Question
            {
                SystemName = "QUESTION_EDUCATION_QUALIFIED2_INDIVIDUAL",
                Text = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL",
                Info = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_INFO"

            };
            var qFounder2_Qualified_Individual_yes = new Choice
            {
                SystemName = "QUESTION_EDUCATION_QUALIFIED2_INDIVIDUAL_YES",
                Text = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_YES",

            };
            var qFounder2_Qualified_Individual_no = new Choice
            {
                SystemName = "QUESTION_EDUCATION_QUALIFIED2_INDIVIDUAL_NO",
                Text = "QUESTION_EDUCATION_QUALIFIED_INDIVIDUAL_NO",

            };
            qFounder2_Qualified_Individual.Choices.AddRange(new Choice[]
            {
                qFounder2_Qualified_Individual_yes,
                qFounder2_Qualified_Individual_no,
            });

            
            qFounderType2.SubQuestions.Add(qFounder2_Qualified_Individual);
            qFounderType2_Individual.NextQuestionID = qFounder2_Qualified_Individual.QuestionID.ToString();

            ////1.1.3.2
            var qFounder2_Qualified_Juristic = new Question
            {
                SystemName = "QUESTION_EDUCATION_QUALIFIED2_JURISTIC",
                Text = "QUESTION_EDUCATION_QUALIFIED_JURISTIC",
                Info = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_INFO"

            };
            var qFounder2_Qualified_Juristic_yes = new Choice
            {
                SystemName = "QUESTION_EDUCATION_QUALIFIED2_JURISTIC_YES",
                Text = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_YES",

            };
            var qFounder2_Qualified_Juristic_no = new Choice
            {
                SystemName = "QUESTION_EDUCATION_QUALIFIED2_JURISTIC_NO",
                Text = "QUESTION_EDUCATION_QUALIFIED_JURISTIC_NO",

            };
            qFounder2_Qualified_Juristic.Choices.AddRange(new Choice[]
            {
                qFounder2_Qualified_Juristic_yes,
                qFounder2_Qualified_Juristic_no,
            });
            qFounderType2.SubQuestions.Add(qFounder2_Qualified_Juristic);
            qFounderType2_Juristic.NextQuestionID = qFounder2_Qualified_Juristic.QuestionID.ToString();



            // Add sign-tax question
            AddSignTaxQuestion(quizEducation, BUSINESS_EDUCATION_TYPE);

			#region Suggestions
			quizEducation.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "ไม่ต้องขออนุญาตจัดตั้งโรงเรียนนอกระบบ",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qTeachingType.SystemName+"_"+qTeachingType_no.SystemName, new string[] { "true" }),

					}
				},
				new Suggestion()
				{
					Message = "ไม่สามารถขออนุญาตได้เนื่องจากไม่เป็นไปตามกฎหมายกำหนด",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qSchool_Location.SystemName, new string[] { qSchool_Location_no.SystemName }),
						new SuggestionCondition(qSchool_Location_Type.SystemName+"_"+qSchool_Location_Type_Condo.SystemName, new string[] { "true" }),
						new SuggestionCondition(qConstruction_Cert.SystemName, new string[] { qConstruction_Cert_no.SystemName }),
						new SuggestionCondition(qSpace.SystemName, new string[] { qSpace_Less100.SystemName }),
						new SuggestionCondition(qSpace2.SystemName, new string[] { qSpace2_Less200.SystemName }),
						new SuggestionCondition(qFounder_Qualified_Individual.SystemName, new string[] { qFounder_Qualified_Individual_no.SystemName }),
						new SuggestionCondition(qFounder_Qualified_Juristic.SystemName, new string[] { qFounder_Qualified_Juristic_no.SystemName }),
                        new SuggestionCondition(qFounder2_Qualified_Individual.SystemName, new string[] { qFounder2_Qualified_Individual_no.SystemName }),
                        new SuggestionCondition(qFounder2_Qualified_Juristic.SystemName, new string[] { qFounder2_Qualified_Juristic_no.SystemName }),
                    }
				},
			};

			#endregion

			db.InsertOne(quizEducation);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_EDUCATION_TYPE,
					AppSystemName =  "APP_EDUCATION_PRIVATE_SCHOOL",


					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						    new QuizAnswer(qFounder_Qualified_Juristic.SystemName, qFounder_Qualified_Juristic_yes.SystemName),
						    new QuizAnswer(qFounder_Qualified_Individual.SystemName, qFounder_Qualified_Individual_yes.SystemName),
						})
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{

					}
				},

                new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_EDUCATION_TYPE,
                    AppSystemName =  "APP_EDUCATION_PRIVATE_SCHOOL",


                    DisplayConditions = new QuizAnswer[] {
                        new QuizAnswerGroup(true, new QuizAnswer[]
                        {
                            new QuizAnswer(qFounder2_Qualified_Juristic.SystemName, qFounder2_Qualified_Juristic_yes.SystemName),
                            new QuizAnswer(qFounder2_Qualified_Individual.SystemName, qFounder2_Qualified_Individual_yes.SystemName),
                        })
                    },

                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {

                    }
                },

            };

			dbAppMapping.InsertMany(items);
		}
		private static void InitElderlyCareQuiz()
		{
			string BUSINESS_ELDERLYCARE_TYPE = BusinessGroupID.ELDERLYCARE;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizElderlyCare = new SmartQuiz(BUSINESS_ELDERLYCARE_TYPE)
			{
				Text = "QUIZ_OPEN_ELDERLYCARE"
			};
			// Add Hospital
			AddHospitalQuestion(quizElderlyCare, BUSINESS_ELDERLYCARE_TYPE);


			// Add sign-tax question
			AddSignTaxQuestion(quizElderlyCare, BUSINESS_ELDERLYCARE_TYPE);
			db.InsertOne(quizElderlyCare);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[] { };


			// Append Hospital app mappings
			items = AddHospitalAppMappings(items, BUSINESS_ELDERLYCARE_TYPE);
			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_ELDERLYCARE_TYPE);

			dbAppMapping.InsertMany(items);
		}
		private static void InitMedicalToolsQuiz()
		{
			string BUSINESS_MEDICALTOOLS_TYPE = BusinessGroupID.MEDICALTOOLS;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizMedicalTools = new SmartQuiz(BUSINESS_MEDICALTOOLS_TYPE)
			{
				Text = "QUIZ_OPEN_MEDICALTOOLS"
			};

			//// 1
			//var qImport = new Question
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT",
			//};
			//var qImport_yes = new Choice
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_YES",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_YES",
			//};
			//var qImport_no = new Choice
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_NO",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_NO",
			//};

			//qImport.Choices.AddRange(new Choice[]
			//{
			//	qImport_yes,
			//	qImport_no,
			//});
			//quizMedicalTools.Questions.Add(qImport);

			////1.1
			//var qProductTarget = new Question
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET",
			//	Info = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_INFO",
			//	AllowMultipleAnswer = true,
			//};
			//var qProductTarget_A = new Choice
			//{
			//	SystemName = "A",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_A",

			//};
			//var qProductTarget_B = new Choice
			//{
			//	SystemName = "B",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_B",

			//};
			//var qProductTarget_C = new Choice
			//{
			//	SystemName = "C",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_C",

			//};
			//var qProductTarget_D = new Choice
			//{
			//	SystemName = "D",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_D",

			//};
			//var qProductTarget_E = new Choice
			//{
			//	SystemName = "E",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_E",

			//};
			//var qProductTarget_F = new Choice
			//{
			//	SystemName = "F",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_F",

			//};
			//var qProductTarget_G = new Choice
			//{
			//	SystemName = "G",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_G",

			//};
			//var qProductTarget_H = new Choice
			//{
			//	SystemName = "H",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_H",

			//};
			//var qProductTarget_no = new Choice
			//{
			//	SystemName = "NO",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_PRODUCT_TARGET_NO",
			//	ForbiddenChoices = new List<Guid>()
			//	{
			//		qProductTarget_A.ChoiceID,
			//		qProductTarget_B.ChoiceID,
			//		qProductTarget_C.ChoiceID,
			//		qProductTarget_D.ChoiceID,
			//		qProductTarget_E.ChoiceID,
			//		qProductTarget_F.ChoiceID,
			//		qProductTarget_G.ChoiceID,
			//		qProductTarget_H.ChoiceID,
			//	}

			//};
			//qProductTarget.Choices.AddRange(new Choice[]
			//{
			//	qProductTarget_A,
			//	qProductTarget_B,
			//	qProductTarget_C,
			//	qProductTarget_D,
			//	qProductTarget_E,
			//	qProductTarget_F,
			//	qProductTarget_G,
			//	qProductTarget_H,
			//	qProductTarget_no,
			//});
			//qImport.SubQuestions.Add(qProductTarget);
			//qImport_yes.NextQuestionID = qProductTarget.QuestionID.ToString();

			////1.1.1
			//var qAchievement = new Question
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_ACHIEVEMENT",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_ACHIEVEMENT",
			//};
			//var qAchievement_yes = new Choice
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_ACHIEVEMENT_YES",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_ACHIEVEMENT_YES",
			//};
			//var qAchievement_no = new Choice
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_ACHIEVEMENT_NO",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_ACHIEVEMENT_NO",
			//};
			//qAchievement.Choices.AddRange(new Choice[]
			//{
			//	qAchievement_yes,
			//	qAchievement_no
			//});
			//qProductTarget.SubQuestions.Add(qAchievement);
			//qProductTarget_A.NextQuestionID = qAchievement.QuestionID.ToString();
			//qProductTarget_B.NextQuestionID = qAchievement.QuestionID.ToString();
			//qProductTarget_C.NextQuestionID = qAchievement.QuestionID.ToString();
			//qProductTarget_D.NextQuestionID = qAchievement.QuestionID.ToString();
			//qProductTarget_E.NextQuestionID = qAchievement.QuestionID.ToString();
			//qProductTarget_F.NextQuestionID = qAchievement.QuestionID.ToString();
			//qProductTarget_G.NextQuestionID = qAchievement.QuestionID.ToString();
			//qProductTarget_H.NextQuestionID = qAchievement.QuestionID.ToString();

			////1.1.1.1
			//var qEquipment = new Question
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT",
			//	AllowMultipleAnswer = true
			//};
			//var qEquipment_Condom = new Choice
			//{
			//	SystemName = "CONDOM",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT_CONDOM",
			//};
			//var qEquipment_ContactLens = new Choice
			//{
			//	SystemName = "CONTACTLENS",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT_CONTACTLENS",
			//};
			//var qEquipment_Gloves = new Choice
			//{
			//	SystemName = "GLOVES",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT_GLOVES",
			//};
			//var qEquipment_BloodBag = new Choice
			//{
			//	SystemName = "BLOODBAG",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT_BLOODBAG",
			//};
			//var qEquipment_HIVTest = new Choice
			//{
			//	SystemName = "HIVTEST",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT_HIVTEST",
			//};
			//var qEquipment_no = new Choice
			//{
			//	SystemName = "NO",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT_NO",
			//	ForbiddenChoices = new List<Guid>()
			//	{
			//		qEquipment_Condom.ChoiceID,
			//		qEquipment_ContactLens.ChoiceID,
			//		qEquipment_Gloves.ChoiceID,
			//		qEquipment_BloodBag.ChoiceID,
			//		qEquipment_HIVTest.ChoiceID,
			//	}
			//};
			//qEquipment.Choices.AddRange(new Choice[]
			//{
			//	qEquipment_Condom,
			//	qEquipment_ContactLens,
			//	qEquipment_Gloves,
			//	qEquipment_BloodBag,
			//	qEquipment_HIVTest,
			//	qEquipment_no
			//});
			//qAchievement.SubQuestions.Add(qEquipment);
			//qAchievement_no.NextQuestionID = qEquipment.QuestionID.ToString();
			////1.1.1.1.1
			//var qEquipment2 = new Question
			//{
			//	SystemName = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT",
			//	AllowMultipleAnswer = true
			//};
			//var qEquipment2_PhysicalTherapy = new Choice
			//{
			//	SystemName = "PHYSICAL_THERAPY",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_PHYSICAL_THERAPY",
			//};
			//var qEquipment2_Alcohol = new Choice
			//{
			//	SystemName = "ALCOHOL",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_ALCOHOL",
			//};
			//var qEquipment2_TeethWhitening = new Choice
			//{
			//	SystemName = "TEETH_WHITENING",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_TEETH_WHITENING",
			//};
			//var qEquipment2_ContactLensCare = new Choice
			//{
			//	SystemName = "CONTACTLENS_CARE",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_CONTACTLENS_CARE",
			//};
			//var qEquipment2_Silicone = new Choice
			//{
			//	SystemName = "SILICONE",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_SILICONE",
			//};
			//var qEquipment2_TightenBreast = new Choice
			//{
			//	SystemName = "TIGHTEN_BREAST",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_TIGHTEN_BREAST",
			//};
			//var qEquipment2_DrugTest = new Choice
			//{
			//	SystemName = "DRUGTEST",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_DRUGTEST",
			//};
			//var qEquipment2_EyeSurgery = new Choice
			//{
			//	SystemName = "EYE_SURGERY",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_EYE_SURGERY",
			//};
			//var qEquipment2_KidneyCleanser = new Choice
			//{
			//	SystemName = "KIDNEY_CLENSER",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_KIDNEY_CLENSER",
			//};
			//var qEquipment2_Insulin = new Choice
			//{
			//	SystemName = "INSULIN",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_INSULIN",
			//};
			//var qEquipment2_no = new Choice
			//{
			//	SystemName = "NO",
			//	Text = "QUESTION_MEDICALTOOLS_IMPORT_EQUIPMENT2_NO",
			//	ForbiddenChoices = new List<Guid>()
			//	{
			//		qEquipment2_PhysicalTherapy.ChoiceID,
			//		qEquipment2_Alcohol.ChoiceID,
			//		qEquipment2_TeethWhitening.ChoiceID,
			//		qEquipment2_ContactLensCare.ChoiceID,
			//		qEquipment2_Silicone.ChoiceID,
			//		qEquipment2_TightenBreast.ChoiceID,
			//		qEquipment2_DrugTest.ChoiceID,
			//		qEquipment2_EyeSurgery.ChoiceID,
			//		qEquipment2_KidneyCleanser.ChoiceID,
			//		qEquipment2_Insulin.ChoiceID,
			//	}
			//};
			//qEquipment2.Choices.AddRange(new Choice[]
			//{
			//	qEquipment2_PhysicalTherapy,
			//	qEquipment2_Alcohol,
			//	qEquipment2_TeethWhitening,
			//	qEquipment2_ContactLensCare,
			//	qEquipment2_Silicone,
			//	qEquipment2_TightenBreast,
			//	qEquipment2_DrugTest,
			//	qEquipment2_EyeSurgery,
			//	qEquipment2_KidneyCleanser,
			//	qEquipment2_Insulin,
			//	qEquipment2_no
			//});

			//qEquipment.SubQuestions.Add(qEquipment2);
			//qEquipment_no.NextQuestionID = qEquipment2.QuestionID.ToString();

			// Add MedicalTools question
			AddMedicalToolsQuestion(quizMedicalTools, BUSINESS_MEDICALTOOLS_TYPE);
			// Add sign-tax question
			AddSignTaxQuestion(quizMedicalTools, BUSINESS_MEDICALTOOLS_TYPE);

			//#region Suggestions
			//quizMedicalTools.Suggestions = new Suggestion[]
			//{
			//	new Suggestion()
			//	{
			//		Message = "กรุณาศึกษานิยามเครื่องมือแพทย์เพิ่มเติมได้ที่ กองควบคุมเครื่องมือแพทย์ สำนักงานคณะกรรมการอาหารและยา <a href='http://web.krisdika.go.th/lawHeadContent.jsp?fromPage=lawHeadContent&formatFile=htm&hID=0'>http://web.krisdika.go.th/lawHeadContent.jsp?fromPage=lawHeadContent&formatFile=htm&hID=0</a>",
			//		RequireAllCondition = false,
			//		Conditions = new SuggestionCondition[]
			//		{

			//			new SuggestionCondition(qProductTarget.SystemName+"_"+qProductTarget_no.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qAchievement.SystemName, new string[] { qAchievement_yes.SystemName }),
			//		}
			//	},
			//	new Suggestion()
			//	{
			//		Message = "กรุณาศึกษาข้อมูลเพิ่มเติมได้ที่ กองควบคุมเครื่องมือแพทย์ สำนักงานคณะกรรมการอาหารและยา <a href='http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx'>http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx</a>",
			//		RequireAllCondition = false,
			//		Conditions = new SuggestionCondition[]
			//		{
			//			new SuggestionCondition(qImport.SystemName, new string[] { qImport_no.SystemName }),
			//		}
			//	},
			//	new Suggestion()
			//	{
			//		Message = "กรุณาติดต่อ อย. เพื่อยื่นคำขอจดทะเบียนสถานประกอบการนําเข้าเครื่องมือแพทย์ และคําขออนุญาตนําเข้าเครื่องมือแพทย์ จากเว็ปไซต์ <a href='http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx'>http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx</a>",
			//		RequireAllCondition = false,
			//		Conditions = new SuggestionCondition[]
			//		{
			//			new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_Condom.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_ContactLens.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_Gloves.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_BloodBag.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_HIVTest.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_PhysicalTherapy.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_Alcohol.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_TeethWhitening.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_ContactLensCare.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_Silicone.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_TightenBreast.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_DrugTest.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_EyeSurgery.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_KidneyCleanser.SystemName, new string[] { "true" }),
			//			new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_Insulin.SystemName, new string[] { "true" }),
			//		}
			//	},
			//};
			//#endregion

			db.InsertOne(quizMedicalTools);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[] { };
			//{
			//	new QuizAppMapping()
			//	{
			//		QuizGroup = BUSINESS_MEDICALTOOLS_TYPE,
			//		AppSystemName =  "หนังสือรับรองประกอบการนำเข้า",

			//		DisplayConditions = new QuizAnswer[] {
			//			new QuizAnswer(qEquipment2.SystemName+"_"+qEquipment2_no.SystemName,"on" )
			//		},

			//		OnlineRequestAllowedConditions = new QuizAnswer[]
			//		{
			//		}
			//	},

			//};

			//Append Medical Tools app mappings
			items = AddMedicalToolsAppMappings(items, BUSINESS_MEDICALTOOLS_TYPE);
			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_MEDICALTOOLS_TYPE);

			dbAppMapping.InsertMany(items);
		}
		private static void InitBeautyClinicQuiz()
		{
			string BUSINESS_BEAUTYCLINIC_TYPE = BusinessGroupID.BEAUTYCLINIC;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizBeautyClinic = new SmartQuiz(BUSINESS_BEAUTYCLINIC_TYPE)
			{
				Text = "QUIZ_OPEN_BEAUTYCLINIC"
			};
			// Add Hospital question
			AddHospitalQuestion(quizBeautyClinic, BUSINESS_BEAUTYCLINIC_TYPE);
			// Add Medical question
			AddMedicalToolsQuestion(quizBeautyClinic, BUSINESS_BEAUTYCLINIC_TYPE);
			// Add sign-tax question
			AddSignTaxQuestion(quizBeautyClinic, BUSINESS_BEAUTYCLINIC_TYPE);
			db.InsertOne(quizBeautyClinic);

			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[] { };
			// Append Hospital app mappings
			items = AddHospitalAppMappings(items, BUSINESS_BEAUTYCLINIC_TYPE);
			// Append Medical tools app mappings
			items = AddMedicalToolsAppMappings(items, BUSINESS_BEAUTYCLINIC_TYPE);
			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_BEAUTYCLINIC_TYPE);
			dbAppMapping.InsertMany(items);
		}
		private static void InitEnergyQuiz()
		{
			string BUSINESS_ENERGY_TYPE = BusinessGroupID.ENERGY;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizEnergy = new SmartQuiz(BUSINESS_ENERGY_TYPE)
			{
				Text = "QUIZ_OPEN_ENERGY"
			};
			// 1
			var qElectric_kVA = new Question
			{
				SystemName = "QUESTION_ENERGY_kVA",
				Text = "QUESTION_ENERGY_kVA",
			};
			var qElectric_kVA_Less200 = new Choice
			{
				SystemName = "QUESTION_ENERGY_kVA_LESS200",
				Text = "QUESTION_ENERGY_kVA_LESS200",
			};
			var qElectric_kVA_More200 = new Choice
			{
				SystemName = "QUESTION_ENERGY_kVA_MORE200",
				Text = "QUESTION_ENERGY_kVA_MORE200",
			};

			qElectric_kVA.Choices.AddRange(new Choice[]
			{
				qElectric_kVA_Less200,
				qElectric_kVA_More200,
			});
			quizEnergy.Questions.Add(qElectric_kVA);

			//1.1
			var qEMbackup = new Question
			{
				SystemName = "QUESTION_ENERGY_EM_BACKUP",
				Text = "QUESTION_ENERGY_EM_BACKUP",


			};
			var qEMbackup_yes = new Choice
			{
				SystemName = "QUESTION_ENERGY_EM_BACKUP_YES",
				Text = "QUESTION_ENERGY_EM_BACKUP_YES",

			};
			var qEMbackup_no = new Choice
			{
				SystemName = "QUESTION_ENERGY_EM_BACKUP_NO",
				Text = "QUESTION_ENERGY_EM_BACKUP_NO",

			};
			qEMbackup.Choices.AddRange(new Choice[]
			{
				qEMbackup_yes,
				qEMbackup_no
			});
			qElectric_kVA.SubQuestions.Add(qEMbackup);
			qElectric_kVA_More200.NextQuestionID = qEMbackup.QuestionID.ToString();

			// 2
			var qProductionCap_Less100 = new Question
			{
				SystemName = "QUESTION_ENERGY_PRODUCTION_CAP_LESS100",
				Text = "QUESTION_ENERGY_PRODUCTION_CAP_LESS100",
			};
			var qProductionCap_Less100_yes = new Choice
			{
				SystemName = "QUESTION_ENERGY_PRODUCTION_CAP_LESS100_YES",
				Text = "QUESTION_ENERGY_PRODUCTION_CAP_LESS100_YES",
			};
			var qProductionCap_Less100_no = new Choice
			{
				SystemName = "QUESTION_ENERGY_PRODUCTION_CAP_LESS100_NO",
				Text = "QUESTION_ENERGY_PRODUCTION_CAP_LESS100_NO",
			};

			qProductionCap_Less100.Choices.AddRange(new Choice[]
			{
				qProductionCap_Less100_yes,
				qProductionCap_Less100_no,
			});
			quizEnergy.Questions.Add(qProductionCap_Less100);

			//3
			//var qRooftop = new Question
			//{
			//	SystemName = "QUESTION_ENERGY_ROOFTOP",
			//	Text = "QUESTION_ENERGY_ROOFTOP",
			//};
			//var qRooftop_yes = new Choice
			//{
			//	SystemName = "QUESTION_ENERGY_ROOFTOP_YES",
			//	Text = "QUESTION_ENERGY_ROOFTOP_YES",
			//};
			//var qRooftop_no = new Choice
			//{
			//	SystemName = "QUESTION_ENERGY_ROOFTOP_NO",
			//	Text = "QUESTION_ENERGY_ROOFTOP_NO",
			//};

			//qRooftop.Choices.AddRange(new Choice[]
			//{
			//	qRooftop_yes,
			//	qRooftop_no,
			//});
			//quizEnergy.Questions.Add(qRooftop);

			////4
			//var qDistributeCap_Less1000 = new Question
			//{
			//	SystemName = "QUESTION_ENERGY_DISTRIBUTE_CAP_LESS1000",
			//	Text = "QUESTION_ENERGY_DISTRIBUTE_CAP_LESS1000",
			//};
			//var qDistributeCap_Less1000_yes = new Choice
			//{
			//	SystemName = "QUESTION_ENERGY_DISTRIBUTE_CAP_LESS1000_YES",
			//	Text = "QUESTION_ENERGY_DISTRIBUTE_CAP_LESS1000_YES",
			//};
			//var qDistributeCap_Less1000_no = new Choice
			//{
			//	SystemName = "QUESTION_ENERGY_DISTRIBUTE_CAP_LESS1000_NO",
			//	Text = "QUESTION_ENERGY_DISTRIBUTE_CAP_LESS1000_NO",
			//};

			//qDistributeCap_Less1000.Choices.AddRange(new Choice[]
			//{
			//	qDistributeCap_Less1000_yes,
			//	qDistributeCap_Less1000_no,
			//});
			//quizEnergy.Questions.Add(qDistributeCap_Less1000);

			////5
			//var qDistributeSize_Less1000 = new Question
			//{
			//	SystemName = "QUESTION_ENERGY_DISTRIBUTE_SIZE_LESS1000",
			//	Text = "QUESTION_ENERGY_DISTRIBUTE_SIZE_LESS1000",
			//};
			//var qDistributeSize_Less1000_yes = new Choice
			//{
			//	SystemName = "QUESTION_ENERGY_DISTRIBUTE_SIZE_LESS1000_YES",
			//	Text = "QUESTION_ENERGY_DISTRIBUTE_SIZE_LESS1000_YES",
			//};
			//var qDistributeSize_Less1000_no = new Choice
			//{
			//	SystemName = "QUESTION_ENERGY_DISTRIBUTE_SIZE_LESS1000_NO",
			//	Text = "QUESTION_ENERGY_DISTRIBUTE_SIZE_LESS1000_NO",
			//};

			//qDistributeSize_Less1000.Choices.AddRange(new Choice[]
			//{
			//	qDistributeSize_Less1000_yes,
			//	qDistributeSize_Less1000_no,
			//});
			//quizEnergy.Questions.Add(qDistributeSize_Less1000);

			#region Suggestions
			quizEnergy.Suggestions = new Suggestion[]
			{
                new Suggestion()
                {
                    Message ="ไม่ต้องขอรับใบอนุญาตให้ผลิตพลังงานควบคุม",
                    RequireAllCondition = false,
                    Conditions = new SuggestionCondition[]
                    {
                        new SuggestionCondition(qElectric_kVA.SystemName, new string[] { qElectric_kVA_Less200.SystemName }),
                    }
                },
                new Suggestion()
                {
                    Message ="กรุณาติดต่อคณะกรรมการกำกับกิจการพลังงาน เพื่อขอใบอนุญาตผลิตพลังงานควบคุม และใบประกอบกิจการผลิตไฟฟ้า และใบแจ้งเริ่มประกอบกิจการผลิตไฟฟ้า [<a href='http://www.erc.or.th/ERCWeb2/'>http://www.erc.or.th/ERCWeb2/</a>]",
                    RequireAllCondition = false,
                    Conditions = new SuggestionCondition[]
                    {
                        new SuggestionCondition(qEMbackup.SystemName, new string[] { qEMbackup_no.SystemName }),
                    }
                },
                new Suggestion()
                {

                    Message ="กรุณาศึกษาข้อมูลเงื่อนไขการยกเว้น จากคณะกรรมการกำกับกิจการพลังงาน <a href='http://www.erc.or.th/ERCWeb2/Upload/Document/%E0%B8%84%E0%B8%B9%E0%B9%88%E0%B8%A1%E0%B8%B7%E0%B8%AD%E0%B8%81%E0%B8%B2%E0%B8%A3%E0%B8%82%E0%B8%AD%E0%B8%A3%E0%B8%B1%E0%B8%9A%E0%B9%83%E0%B8%9A%E0%B8%AD%E0%B8%99%E0%B8%B8%E0%B8%8D%E0%B8%B2%E0%B8%95_%E0%B8%A3%E0%B8%A7%E0%B8%A1.pdf'>http://www.erc.or.th/ERCWeb2/Upload/Document/คู่มือการขอรับใบอนุญาต_รวม.pdf</a>",
                    RequireAllCondition = false,
                    Conditions = new SuggestionCondition[]
                    {
                        new SuggestionCondition(qProductionCap_Less100.SystemName, new string[] { qProductionCap_Less100_no.SystemName }),

                    }
                },
            };

			#endregion

			db.InsertOne(quizEnergy);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ENERGY_TYPE,
					AppSystemName =  AppSystemNameTextConst.APP_ENERGY_PRODUCTION,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qEMbackup.SystemName, qEMbackup_yes.SystemName)
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},

                new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_ENERGY_TYPE,
                    AppSystemName =  AppSystemNameTextConst.APP_ENERGY_PRODUCTION_NOT_PERMIT,
                    DisplayConditions = new QuizAnswer[] {
                        new QuizAnswer(qProductionCap_Less100.SystemName, qProductionCap_Less100_yes.SystemName)
                    },

                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    }
                },
            };


			dbAppMapping.InsertMany(items);
		}
		private static void InitOrganicFarmingQuiz()
		{
			string BUSINESS_ORGANICFARMING_TYPE = BusinessGroupID.ORGANICFARMING;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizOrganic = new SmartQuiz(BUSINESS_ORGANICFARMING_TYPE)
			{
				Text = "QUIZ_OPEN_ORGANICFARMING"
			};

			// 1
			var qOrganicFarming = new Question
			{
				SystemName = "QUESTION_ORGANIC_FARMING",
				Text = "QUESTION_ORGANIC_FARMING",

			};

			var qOrganicFarming_yes = new Choice
			{
				SystemName = "QUESTION_ORGANIC_FARMING_YES",
				Text = "QUESTION_ORGANIC_FARMING_YES",
			};
			var qOrganicFarming_no = new Choice
			{
				SystemName = "QUESTION_ORGANIC_FARMING_NO",
				Text = "QUESTION_ORGANIC_FARMING_NO",
			};

			qOrganicFarming.Choices.AddRange(new Choice[]
			{
				qOrganicFarming_yes,
				qOrganicFarming_no,
			});
			quizOrganic.Questions.Add(qOrganicFarming);

			//1.1
			var qOrganic_Act = new Question
			{
				SystemName = "QUESTION_ORGANIC_ACT",
				Text = "QUESTION_ORGANIC_ACT",
				AllowMultipleAnswer = true

			};			
			var qOrganic_Act_Packing = new Choice
			{
				SystemName = "PACKING",
				Text = "QUESTION_ORGANIC_ACT_PACKING",

			};
			var qOrganic_Act_Transform = new Choice
			{
				SystemName = "TRANSFORM",
				Text = "QUESTION_ORGANIC_ACT_TRANSFORM",

			};
			var qOrganic_Act_Collect = new Choice
			{
				SystemName = "COLLECT",
				Text = "QUESTION_ORGANIC_ACT_COLLECT",

			};
			var qOrganic_Act_Sell = new Choice
			{
				SystemName = "SELL",
				Text = "QUESTION_ORGANIC_ACT_SELL",

			};
			var qOrganic_Act_Import = new Choice
			{
				SystemName = "IMPORT",
				Text = "QUESTION_ORGANIC_ACT_IMPORT",

			};
            var qOrganic_Act_Source = new Choice
            {
                SystemName = "SOURCE",
                Text = "QUESTION_ORGANIC_ACT_SOURCE",
                ForbiddenChoices = new List<Guid>()
                {
                    qOrganic_Act_Packing.ChoiceID,
                    qOrganic_Act_Transform.ChoiceID,
                    qOrganic_Act_Collect.ChoiceID,
                    qOrganic_Act_Sell.ChoiceID,
                    qOrganic_Act_Import.ChoiceID,
                }
            };
            qOrganic_Act.Choices.AddRange(new Choice[]
			{
				qOrganic_Act_Source,
				qOrganic_Act_Packing,
				qOrganic_Act_Transform,
				qOrganic_Act_Collect,
				qOrganic_Act_Sell,
				qOrganic_Act_Import
			});
			qOrganicFarming.SubQuestions.Add(qOrganic_Act);
			qOrganicFarming_yes.NextQuestionID = qOrganic_Act.QuestionID.ToString();



			#region Suggestions
			quizOrganic.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาศึกษานิยามของเกษตรอินทรีย์ ได้จากประกาศกระทรวงเกษตรและสหกรณ์ ตามพรบ.มาตรฐานสินค้าเกษตร พศ. 2551 <a href='https://www.acfs.go.th/standard/download/GUIDANCE_ORGANIC-PART-1_PRODUCTS-FROM-ORGANIC.pdf'>https://www.acfs.go.th/standard/download/GUIDANCE_ORGANIC-PART-1_PRODUCTS-FROM-ORGANIC.pdf</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qOrganicFarming.SystemName, new string[] { qOrganicFarming_no.SystemName }),
					}
				},
				new Suggestion()
				{
					Message = "ยังไม่เปิดให้บริการ กรุณาติดต่อ กรมวิชาการเกษตร <a href='http://www.doa.go.th/main/?option=com_content&view=article&id=87&Itemid=109'>http://www.doa.go.th/main/?option=com_content&view=article&id=87&Itemid=109	</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qOrganic_Act.SystemName+"_"+qOrganic_Act_Collect.SystemName, new string[] { "true" }),
						new SuggestionCondition(qOrganic_Act.SystemName+"_"+qOrganic_Act_Packing.SystemName, new string[] { "true"  }),
						new SuggestionCondition(qOrganic_Act.SystemName+"_"+qOrganic_Act_Sell.SystemName, new string[] {"true"   }),
						new SuggestionCondition(qOrganic_Act.SystemName+"_"+qOrganic_Act_Import.SystemName, new string[] { "true"  }),
						new SuggestionCondition(qOrganic_Act.SystemName+"_"+qOrganic_Act_Transform.SystemName, new string[] { "true"  }),

					}
				},
			};

			#endregion

			db.InsertOne(quizOrganic);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ORGANICFARMING_TYPE,
					AppSystemName =  AppSystemNameTextConst.APP_ORGANIC_PLANT_PRODUCTION_NEW,


					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qOrganic_Act.SystemName+"_"+qOrganic_Act_Source.SystemName, "on"),
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},


			};

			dbAppMapping.InsertMany(items);
		}
		private static void InitSmallAgriculturalQuiz()
		{
			string BUSINESS_SMALLAGRICULTURAL_TYPE = BusinessGroupID.SMALLAGRICULTURAL;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizSmallAgricultural = new SmartQuiz(BUSINESS_SMALLAGRICULTURAL_TYPE)
			{
				Text = "QUIZ_OPEN_SMALLAGRICULTURAL"
			};

			// 1
			var qWorker = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_WORKER",
				Text = "QUESTION_COMMON_WORKER",

			};

			var qWorker_Less50 = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_WORKER_LESS50",
				Text = "QUESTION_COMMON_WORKER_LESS50",
			};
			var qWorker_More50 = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_WORKER_MORE50",
				Text = "QUESTION_COMMON_WORKER_MORE50",
			};

			qWorker.Choices.AddRange(new Choice[]
			{   qWorker_Less50,
				qWorker_More50
			});
			quizSmallAgricultural.Questions.Add(qWorker);
			//1.1
			var qSugarBusiness = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_SUGAR",
				Text = "QUESTION_SMALLAGRICULTURAL_SUGAR",

			};

			var qSugarBusiness_yes = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_SUGAR_YES",
				Text = "QUESTION_SMALLAGRICULTURAL_SUGAR_YES",
			};
			var qSugarBusiness_no = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_SUGAR_NO",
				Text = "QUESTION_SMALLAGRICULTURAL_SUGAR_NO",
			};

			qSugarBusiness.Choices.AddRange(new Choice[]
			{
				qSugarBusiness_yes,
				qSugarBusiness_no,
			});
			qWorker.SubQuestions.Add(qSugarBusiness);
			qWorker_More50.NextQuestionID = qSugarBusiness.QuestionID.ToString();

			//1.1.1
			var qBusinessType = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_BUSINESS_TYPE",
				Text = "QUESTION_SMALLAGRICULTURAL_FACTORY_TYPE",
				AllowMultipleAnswer = true

			};
			var qConveyingStones = new Choice
			{
				SystemName = "CONVEYING_STONES",
				Text = "QUESTION_SMALLAGRICULTURAL_BUSINESS_TYPE_CONVEYING_STONES",

			};
			var qSandSuction = new Choice
			{
				SystemName = "SAND_SUCTION",
				Text = "QUESTION_SMALLAGRICULTURAL_BUSINESS_TYPE_SAND_SUCTION",

			};
			var qBurningJewel = new Choice
			{
				SystemName = "BURNING_JEWEL",
				Text = "QUESTION_SMALLAGRICULTURAL_BUSINESS_TYPE_BURNING_JEWEL",

			};
			var qBusiness_Other = new Choice
			{
				SystemName = "OTHER",
				Text = "QUESTION_SMALLAGRICULTURAL_BUSINESS_TYPE_OTHER",
				ForbiddenChoices = new List<Guid>()
				{
					qConveyingStones.ChoiceID,
					qSandSuction.ChoiceID,
					qBurningJewel.ChoiceID,
				}
			};

			qBusinessType.Choices.AddRange(new Choice[]
			{
				qConveyingStones,
				qSandSuction,
				qBurningJewel,
				qBusiness_Other
			});
			qSugarBusiness.SubQuestions.Add(qBusinessType);
			qSugarBusiness_no.NextQuestionID = qBusinessType.QuestionID.ToString();

			//1.1.1.1
			var qFuel = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_FUEL",
				Text = "QUESTION_SMALLAGRICULTURAL_FUEL",

			};

			var qFuel_yes = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_FUEL_YES",
				Text = "QUESTION_SMALLAGRICULTURAL_FUEL_YES",
			};
			var qFuel_no = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_FUEL_NO",
				Text = "QUESTION_SMALLAGRICULTURAL_FUEL_NO",
			};

			qFuel.Choices.AddRange(new Choice[]
			{
				qFuel_yes,
				qFuel_no,
			});
			qBusinessType.SubQuestions.Add(qFuel);
			qBusiness_Other.NextQuestionID = qFuel.QuestionID.ToString();
			//1.1.1.1.1
			var qGroup_Type = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_GROUP_TYPE",
				Text = "QUESTION_SMALLAGRICULTURAL_GROUP_TYPE",

			};

			var qPlant = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_GROUP_TYPE_PLANT",
				Text = "QUESTION_SMALLAGRICULTURAL_GROUP_TYPE_PLANT",
			};
			var qAnimal = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_GROUP_TYPE_ANIMAL",
				Text = "QUESTION_SMALLAGRICULTURAL_GROUP_TYPE_ANIMAL",
			};

			qGroup_Type.Choices.AddRange(new Choice[]
			{
				qPlant,
				qAnimal,
			});
			qFuel.SubQuestions.Add(qGroup_Type);
			qFuel_no.NextQuestionID = qGroup_Type.QuestionID.ToString();

			//1.1.1.1.1.1  qFactoryTypeA
			List<Choice> qFactoryTypeA_Item = new List<Choice>();
			for (int i = 1; i < 21; i++)
			{
				var c = new Choice
				{
					SystemName = "ITEM" + i,
					Text = "QUESTION_SMALLAGRICULTURAL_FACTORY_A_ITEM" + i,
				};
				qFactoryTypeA_Item.Add(c);
			}

			var qFactoryTypeANone = new Choice
			{
				SystemName = "ITEM_None",
				Text = "QUESTION_SMALLAGRICULTURAL_FACTORY_A_ITEM_NONE",
				ForbiddenChoices = qFactoryTypeA_Item.Select(x => x.ChoiceID).ToList(),
			};
			qFactoryTypeA_Item.Add(qFactoryTypeANone);

			var qFactoryType_A = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_FACTORY_TYPE_A",
				Text = "QUESTION_SMALLAGRICULTURAL_FACTORY_TYPE",
				Info = "QUESTION_SMALLAGRICULTURAL_FACTORY_TYPE_INFO",
				Choices = qFactoryTypeA_Item,
				AllowMultipleAnswer = true

			};
			qGroup_Type.SubQuestions.Add(qFactoryType_A);
			qPlant.NextQuestionID = qFactoryType_A.QuestionID.ToString();

			//1.1.1.1.1.2   qFactoryTypeB
			List<Choice> qFactoryTypeB_Item = new List<Choice>();
			for (int i = 1; i < 12; i++)
			{
				var c = new Choice
				{
					SystemName = "ITEM" + i,
					Text = "QUESTION_SMALLAGRICULTURAL_FACTORY_B_ITEM" + i,
				};
				qFactoryTypeB_Item.Add(c);
			}

			var qFactoryTypeBNone = new Choice
			{
				SystemName = "ITEM_None",
				Text = "QUESTION_SMALLAGRICULTURAL_FACTORY_B_ITEM_NONE",
				ForbiddenChoices = qFactoryTypeB_Item.Select(x => x.ChoiceID).ToList(),
			};
			qFactoryTypeB_Item.Add(qFactoryTypeBNone);

			var qFactoryType_B = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_FACTORY_TYPE_B",
				Text = "QUESTION_SMALLAGRICULTURAL_FACTORY_TYPE",
				Info = "QUESTION_SMALLAGRICULTURAL_FACTORY_TYPE_INFO",
				Choices = qFactoryTypeB_Item,
				AllowMultipleAnswer = true

			};
			qGroup_Type.SubQuestions.Add(qFactoryType_B);
			qAnimal.NextQuestionID = qFactoryType_B.QuestionID.ToString();

			//2
			var qPrivatizeType = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_PRIVATIZE",
				Text = "QUESTION_SMALLAGRICULTURAL_PRIVATIZE",
				AllowMultipleAnswer = true
			};
			var qProducer = new Choice
			{
				SystemName = "PRODUCER",
				Text = "QUESTION_SMALLAGRICULTURAL_PRIVATIZE_PRODUCER",
			};
			var qImporter = new Choice
			{
				SystemName = "IMPORTER",
				Text = "QUESTION_SMALLAGRICULTURAL_PRIVATIZE_IMPORTER",
			};
			var qHirer = new Choice
			{
				SystemName = "HIRER",
				Text = "QUESTION_SMALLAGRICULTURAL_PRIVATIZE_HIRER",
			};

			qPrivatizeType.Choices.AddRange(new Choice[]
			{   qProducer,
				qImporter,
				qHirer,
			});
			quizSmallAgricultural.Questions.Add(qPrivatizeType);

			//2.1
			var qProduct = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_PRODUCT",
				Text = "QUESTION_SMALLAGRICULTURAL_PRODUCT",
				AllowMultipleAnswer = true
			};
			var qStandard = new Choice
			{
				SystemName = "STANDARD",
				Text = "QUESTION_SMALLAGRICULTURAL_PRODUCT_STANDARD",
			};
			var qLebel = new Choice
			{
				SystemName = "LEBEL",
				Text = "QUESTION_SMALLAGRICULTURAL_PRODUCT_LEBEL",
			};
			var qGMP = new Choice
			{
				SystemName = "GMP",
				Text = "QUESTION_SMALLAGRICULTURAL_PRODUCT_GMP",
			};
			var qProduct_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_SMALLAGRICULTURAL_PRODUCT_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qStandard.ChoiceID,
					qLebel.ChoiceID,
					qGMP.ChoiceID,
				}
			};
			qProduct.Choices.AddRange(new Choice[]
			{   qStandard,
				qLebel,
				qGMP,
				qProduct_no
			});

			qPrivatizeType.SubQuestions.Add(qProduct);
			qProducer.NextQuestionID = qProduct.QuestionID.ToString();
			qImporter.NextQuestionID = qProduct.QuestionID.ToString();

			var qAdvertise = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_AD",
				Text = "QUESTION_SMALLAGRICULTURAL_AD",

			};
			var qAdvertise_yes = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_AD_YES",
				Text = "QUESTION_SMALLAGRICULTURAL_AD_YES",
			};
			var qAdvertise_no = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_AD_NO",
				Text = "QUESTION_SMALLAGRICULTURAL_AD_NO",
			};
			qAdvertise.Choices.AddRange(new Choice[]
			{   qAdvertise_yes,
				qAdvertise_no,

			});
			quizSmallAgricultural.Questions.Add(qAdvertise);
			var qAd_Benefit = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_AD_BENEFIT",
				Text = "QUESTION_SMALLAGRICULTURAL_AD_BENEFIT",

			};
			var qAd_Benefit_yes = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_AD_BENEFIT_YES",
				Text = "QUESTION_SMALLAGRICULTURAL_AD_BENEFIT_YES",
			};
			var qAd_Benefit_no = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_AD_BENEFIT_NO",
				Text = "QUESTION_SMALLAGRICULTURAL_AD_BENEFIT_NO",
			};
			qAd_Benefit.Choices.AddRange(new Choice[]
			{   qAd_Benefit_yes,
				qAd_Benefit_no,

			});
			qAdvertise.SubQuestions.Add(qAd_Benefit);
			qAdvertise_yes.NextQuestionID = qAd_Benefit.QuestionID.ToString();

			//4
			var qExport = new Question
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_EXPORT",
				Text = "QUESTION_SMALLAGRICULTURAL_EXPORT",

			};
			var qExport_yes = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_EXPORT_YES",
				Text = "QUESTION_SMALLAGRICULTURAL_EXPORT_YES",
			};
			var qExport_no = new Choice
			{
				SystemName = "QUESTION_SMALLAGRICULTURAL_EXPORT_NO",
				Text = "QUESTION_SMALLAGRICULTURAL_EXPORT_NO",
			};
			qExport.Choices.AddRange(new Choice[]
			{   qExport_yes,
				qExport_no,

			});
			quizSmallAgricultural.Questions.Add(qExport);

			//5  add Sign Tax
			AddSignTaxQuestion(quizSmallAgricultural, BUSINESS_SMALLAGRICULTURAL_TYPE);

			#region Suggestions
			quizSmallAgricultural.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาศึกษาประเภทและลำดับโรงงาน ตาม พรบ.โรงงาน <a href='http://www.diw.go.th/hawk/data/factype.php'> http://www.diw.go.th/hawk/data/factype.php</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qFactoryType_A.SystemName+"_"+qFactoryTypeANone.SystemName, new string[] { "true" }),
						new SuggestionCondition(qFactoryType_B.SystemName+"_"+qFactoryTypeBNone.SystemName, new string[] { "true" }),
                        new SuggestionCondition(qFactoryType_B.SystemName+"_"+qFactoryTypeBNone.SystemName, new string[] { "true" }),
                    }
				},
				new Suggestion()
				{
					Message = "กรุณาศึกษาข้อมูลเพิ่มเติมเรื่องการขอหนังสือรับรอง <a href='http://food.fda.moph.go.th/ESub/document/manual/Certificate.pdf'> http://food.fda.moph.go.th/ESub/document/manual/Certificate.pdf </a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qExport.SystemName, new string[] { qExport_no.SystemName}),
					}
				},
				new Suggestion()
				{
					Message = "กรุณาศึกษาข้อมูลเพิ่มเติมเรื่องการโฆษณาอาหาร <a href='http://food.fda.moph.go.th/law/data/announ_fda/61_advertise.pdf'> http://food.fda.moph.go.th/law/data/announ_fda/61_advertise.pdf </a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qAdvertise.SystemName, new string[] { qAdvertise_no.SystemName}),
						new SuggestionCondition(qAd_Benefit.SystemName, new string[] { qAd_Benefit_no.SystemName}),
					}
				},
				new Suggestion()
				{
					Message = "กรุณติดต่อสอบถามเพิ่มเติมได้ที่สำนักงานคณะกรรมการอาหารและยา <a href='http://www.fda.moph.go.th/Shared%20Documents/คู่มือประชาชน/รวมสำนักอาหารและสำนักด่าน.pdf'> รวมสำนักอาหารและสำนักด่าน.pdf </a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qProduct.SystemName+"_"+qProduct_no.SystemName, new string[] { "true" }),
					}
				},
				new Suggestion()
				{
					Message = "กรุณาติดต่อที่สำนักงานคณะกรรมการอาหารและยา <a href='http://www.fda.moph.go.th/sites/Food/Pages/Main.aspx'> http://www.fda.moph.go.th/sites/Food/Pages/Main.aspx </a> เพื่อยื่นคำขอใบจดทะเบียน/แจ้งรายละเอียดอาหาร (สบ.7)",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qProduct.SystemName+"_"+qLebel.SystemName, new string[] { "true" }),
						new SuggestionCondition(qProduct.SystemName+"_"+qGMP.SystemName, new string[] { "true" }),
						new SuggestionCondition(qProduct.SystemName+"_"+qStandard.SystemName, new string[] { "true" }),
					}
				},
				new Suggestion()
				{
					Message = "กรุณาตรวจสอบเงื่อนไขจากเอกสาร สบ.7 <a href='http://www.fda.moph.go.th/Shared%20Documents/คู่มือประชาชน/รวมสำนักอาหารและสำนักด่าน.pdf'> รวมสำนักอาหารและสำนักด่าน.pdf</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qPrivatizeType.SystemName+"_"+qHirer.SystemName, new string[] { "true" }),

					}
				},
				new Suggestion()
				{
					Message = "ธุรกิจของคุณไม่เข้าข่ายเป็นโรงงาน ซึ่งไม่จำเป็นต้องยื่นคำขอใบอนุญาต",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qWorker.SystemName, new string[] { qWorker_Less50.SystemName }),
						new SuggestionCondition(qSugarBusiness.SystemName, new string[] { qSugarBusiness_yes.SystemName }),
					}
				},
			};

			#endregion

			db.InsertOne(quizSmallAgricultural);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAnswer[] QuizAns = new QuizAnswer[] { };
			List<QuizAnswer> AnsItems = new List<QuizAnswer>();
			foreach (var c in qFactoryTypeA_Item)
			{
				if (c != qFactoryTypeANone)
				{
					AnsItems.Add(QuizAnswerFactory.AnswerChecked(qFactoryType_A, c));
				}
			}
			foreach (var c in qFactoryTypeB_Item)
			{
				if (c != qFactoryTypeBNone)
				{
					AnsItems.Add(QuizAnswerFactory.AnswerChecked(qFactoryType_B, c));
				}
			}
			AnsItems.Add(new QuizAnswer(qFuel.SystemName, qFuel_yes.SystemName));
			QuizAns = AnsItems.ToArray();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
			new QuizAppMapping()
			{
				QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
				AppSystemName = AppSystemNameTextConst.APP_FACTORY_TYPE3, //"หนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 3",
				DisplayConditions = new QuizAnswer[] {

					new QuizAnswerGroup(true,QuizAns)
					},

				OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
			},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_FACTORY_CLASS_3_NEW, //"ใบอนุญาตประกอบกิจการโรงงานจำพวก 3",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswerGroup(true,QuizAns)

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_FACTORY_CLASS_3_START_NEW, //"ใบแจ้งเริ่มประกอบกิจการโรงงาน (จำพวกที่ 3)",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswerGroup(true,QuizAns)

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_FACTORY_TYPE2,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						new QuizAnswer(qBusinessType.SystemName+"_"+qConveyingStones.SystemName, "on"),
						new QuizAnswer(qBusinessType.SystemName+"_"+qSandSuction.SystemName, "on"),
						new QuizAnswer(qBusinessType.SystemName+"_"+qBurningJewel.SystemName, "on"),
						})

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_FACTORY_CLASS_2_NEW,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						new QuizAnswer(qBusinessType.SystemName+"_"+qConveyingStones.SystemName, "on"),
						new QuizAnswer(qBusinessType.SystemName+"_"+qSandSuction.SystemName, "on"),
						new QuizAnswer(qBusinessType.SystemName+"_"+qBurningJewel.SystemName, "on"),
						})

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_FOOD_CERTIFICATE, // "หนังสือรับรองผลิตภัณฑ์อาหาร (Certificate of Free Sale)",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qExport.SystemName,qExport_yes.SystemName),


					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_FOOD_PRODUCTION,// "ใบอนุญาตผลิตอาหาร",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qPrivatizeType.SystemName+"_"+qProducer.SystemName, "on"),


					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_IMPORT_FOOD,// "ใบอนุญาตนำเข้าอาหาร",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qPrivatizeType.SystemName+"_"+qImporter.SystemName, "on"),


					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			new QuizAppMapping()
				{
					QuizGroup = BUSINESS_SMALLAGRICULTURAL_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_FOOD_ADVERTISEMENT,// "ใบอนุญาตโฆษณาอาหาร (ฆอ.2)",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(qAd_Benefit.SystemName,qAd_Benefit_yes.SystemName),
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_SMALLAGRICULTURAL_TYPE);

			dbAppMapping.InsertMany(items);
		}
		private static void InitEcomClothesQuiz()
		{
			string BUSINESS_ECOMCLOTHES_TYPE = BusinessGroupID.ECOMCLOTHES;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizEcomClothes = new SmartQuiz(BUSINESS_ECOMCLOTHES_TYPE)
			{
				Text = "QUIZ_OPEN_ECOMCLOTHES"
			};

			//add Closthes questions
			AddClothesQuestion(quizEcomClothes, BUSINESS_ECOMCLOTHES_TYPE);

			db.InsertOne(quizEcomClothes);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[] { };
			items = AddClothesAppMappings(items, BUSINESS_ECOMCLOTHES_TYPE);
			dbAppMapping.InsertMany(items);
		}
		private static void InitOnlineCosmeticQuiz()
		{
			string BUSINESS_ONLINECOSMETIC_TYPE = BusinessGroupID.ONLINECOSMETIC;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizOnlineCosmetic = new SmartQuiz(BUSINESS_ONLINECOSMETIC_TYPE)
			{
				Text = "QUIZ_OPEN_ONLINECOSMETIC"
			};

			//add Closthes questions
			AddClothesQuestion(quizOnlineCosmetic, BUSINESS_ONLINECOSMETIC_TYPE);

			var qCosmetic = new Question
			{
				SystemName = "QUESTION_ONLINE_COSMETIC_PRODUCER",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER",
				Info = "QUESTION_RETAIL_COSMETIC_PRODUCER_INFO"
			};
			var qCosmetic_yes = new Choice
			{
				SystemName = "QUESTION_ONLINE_COSMETIC_PRODUCER_YES",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER__YES",
			};
			var qCosmetic_no = new Choice
			{
				SystemName = "QUESTION_ONLINE_COSMETIC_PRODUCER_NO",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER__NO",
			};
			qCosmetic.Choices.AddRange(new Choice[]
			{
				qCosmetic_yes,
				qCosmetic_no,
			});
			var qCosmeticPermit = new Question
			{
				SystemName = "QUESTION_ONLINE_COSMETIC_PRODUCER_PERMIT",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT",
			};
			var qCosmeticPermit_yes = new Choice
			{
				SystemName = "QUESTION_ONLINE_COSMETIC_PRODUCER_PERMIT_YES",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT__YES",
			};
			var qCosmeticPermit_no = new Choice
			{
				SystemName = "QUESTION_ONLINE_COSMETIC_PRODUCER_PERMIT_NO",
				Text = "QUESTION_RETAIL_COSMETIC_PRODUCER_PERMIT__NO",
			};
			qCosmeticPermit.Choices.AddRange(new Choice[]
			{
				qCosmeticPermit_yes,
				qCosmeticPermit_no,
			});
			qCosmetic.SubQuestions.Add(qCosmeticPermit);
			qCosmetic_yes.NextQuestionID = qCosmeticPermit.QuestionID.ToString();
			quizOnlineCosmetic.Questions.Add(qCosmetic);

			db.InsertOne(quizOnlineCosmetic);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[] {

				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_ONLINECOSMETIC_TYPE,
					AppSystemName =  AppSystemNameTextConst.APP_COSMETIC, //"ขอจดแจ้งรายละเอียดการผลิตเพื่อขายหรือนำเข้าเพื่อขายเครื่องสำอางควบคุม",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						new QuizAnswer(qCosmetic.SystemName, qCosmetic_yes.SystemName),
						new QuizAnswer(qCosmeticPermit.SystemName, qCosmeticPermit_no.SystemName),
						})
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},

			};
			items = AddClothesAppMappings(items, BUSINESS_ONLINECOSMETIC_TYPE);
			dbAppMapping.InsertMany(items);
		}
		private static void InitCosmeticsQuiz()
		{
			string BUSINESS_COSMETICS_TYPE = BusinessGroupID.COSMETICS;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizCosmetics = new SmartQuiz(BUSINESS_COSMETICS_TYPE)
			{
				Text = "QUIZ_OPEN_COSMETICS"
			};

			// 1
			var qCosmeticsType = new Question
			{
				SystemName = "QUESTION_COSMETICS_TYPE",
				Text = "QUESTION_COSMETICS_TYPE",
				AllowMultipleAnswer = true,
			};

			var qToothpaste = new Choice
			{
				SystemName = "TOOTHPASTE",
				Text = "QUESTION_COSMETICS_TYPE_TOOTHPASTE",
			};
			var qSoap = new Choice
			{
				SystemName = "SOAP",
				Text = "QUESTION_COSMETICS_TYPE_SOAP",
			};
			var qCosmetics = new Choice
			{
				SystemName = "COSMETICS",
				Text = "QUESTION_COSMETICS_TYPE_COSMETICS",
			};
			var qOther = new Choice
			{
				SystemName = "OTHER",
				Text = "QUESTION_COSMETICS_TYPE_OTHER",
				ForbiddenChoices = new List<Guid>()
				{
					qToothpaste.ChoiceID,
					qSoap.ChoiceID,
					qCosmetics.ChoiceID,
				}
			};
			qCosmeticsType.Choices.AddRange(new Choice[]
			{   qToothpaste,
				qSoap,
				qCosmetics,
				qOther
			});
			quizCosmetics.Questions.Add(qCosmeticsType);
			// 2
			var qProducer_Permit = new Question
			{
				SystemName = "QUESTION_COSMETIC_PRODUCER_PERMIT",
				Text = "QUESTION_COSMETIC_PRODUCER_PERMIT",

			};

			var qProducer_Permit_yes = new Choice
			{
				SystemName = "QUESTION_COSMETIC_PRODUCER_PERMIT_YES",
				Text = "QUESTION_COSMETIC_PRODUCER_PERMIT_YES",
			};
			var qProducer_Permit_no = new Choice
			{
				SystemName = "QUESTION_COSMETIC_PRODUCER_PERMIT_NO",
				Text = "QUESTION_COSMETIC_PRODUCER_PERMIT_NO",
			};

			qProducer_Permit.Choices.AddRange(new Choice[]
			{   qProducer_Permit_yes,
				qProducer_Permit_no
			});
			quizCosmetics.Questions.Add(qProducer_Permit);

			//3
			var qWorker = new Question
			{
				SystemName = "QUESTION_COSMETIC_WORKER",
				Text = "QUESTION_COMMON_WORKER",

			};

			var qWorker_Less50 = new Choice
			{
				SystemName = "QUESTION_COSMETIC_WORKER_LESS50",
				Text = "QUESTION_COMMON_WORKER_LESS50",
			};
			var qWorker_More50 = new Choice
			{
				SystemName = "QUESTION_COSMETIC_WORKER_MORE50",
				Text = "QUESTION_COMMON_WORKER_MORE50",
			};

			qWorker.Choices.AddRange(new Choice[]
			{   qWorker_Less50,
				qWorker_More50
			});
			quizCosmetics.Questions.Add(qWorker);

			//4 Add sign-tax question
			AddSignTaxQuestion(quizCosmetics, BUSINESS_COSMETICS_TYPE);


			#region Suggestions
			quizCosmetics.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "ธุรกิจของคุณไม่เข้าข่ายเป็นโรงงาน ซึ่งไม่จำเป็นต้องยื่นใบอนุญาตประกอบกิจการโรงงานจำพวก 3",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qWorker.SystemName, new string[] { qWorker_Less50.SystemName }),
					}
				},

			};

			#endregion

			db.InsertOne(quizCosmetics);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================
			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_COSMETICS_TYPE,

					AppSystemName = "APP_FRT_NEW_001",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						new QuizAnswer(qCosmeticsType.SystemName+"_"+qToothpaste.SystemName, "on"),
						new QuizAnswer(qCosmeticsType.SystemName+"_"+qSoap.SystemName, "on"),
						new QuizAnswer(qCosmeticsType.SystemName+"_"+qCosmetics.SystemName, "on"),
						})


					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_COSMETICS_TYPE,

					AppSystemName = AppSystemNameTextConst.APP_COMMERCIAL,

					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						new QuizAnswer(qCosmeticsType.SystemName+"_"+qToothpaste.SystemName, "on"),
						new QuizAnswer(qCosmeticsType.SystemName+"_"+qSoap.SystemName, "on"),
						new QuizAnswer(qCosmeticsType.SystemName+"_"+qCosmetics.SystemName, "on"),
						})
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = BUSINESS_COSMETICS_TYPE,
					AppSystemName = AppSystemNameTextConst.APP_COSMETIC,//"ขอจดแจ้งรายละเอียดการผลิตเพื่อขายหรือนำเข้าเพื่อขายเครื่องสำอางควบคุม",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qProducer_Permit.SystemName, qProducer_Permit_no.SystemName),

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				 },
				new QuizAppMapping()
					{
					QuizGroup = BUSINESS_COSMETICS_TYPE,
					AppSystemName = "หนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 3",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qWorker.SystemName, qWorker_More50.SystemName),

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
					{
					QuizGroup = BUSINESS_COSMETICS_TYPE,
					AppSystemName = "ใบอนุญาตประกอบกิจการโรงงานจำพวก 3",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qWorker.SystemName, qWorker_More50.SystemName),

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
					{
					QuizGroup = BUSINESS_COSMETICS_TYPE,
					AppSystemName = "ใบแจ้งเริ่มประกอบกิจการโรงงาน (จำพวกที่ 3)",
					DisplayConditions = new QuizAnswer[] {

						new QuizAnswer(qWorker.SystemName, qWorker_More50.SystemName),

					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};
			// Append sign-tax app mappings
			items = AddSignTaxAppMappings(items, BUSINESS_COSMETICS_TYPE);

			dbAppMapping.InsertMany(items);
		}
		private static void InitFinanceQuiz()
		{
			string BUSINESS_FINANCE_TYPE = BusinessGroupID.FINANCE;
			var db = MongoFactory.GetSmartQuizCollection();
			var quizFinance = new SmartQuiz(BUSINESS_FINANCE_TYPE)
			{
				Text = "QUIZ_OPEN_FINANCE"
			};

			// 1
			var qFinanceAct = new Question
			{
				SystemName = "QUESTION_FINANCE_ACT",
				Text = "QUESTION_FINANCE_ACT",
				AllowMultipleAnswer = true,
                Info = "QUESTION_FINANCE_ACT_INFO",
                DefaultShowInfo = true
            };

			var qConsultant = new Choice
			{
				SystemName = "CONSULTANT",
				Text = "QUESTION_FINANCE_ACT_CONSULTANT",
			};
			var qStock = new Choice
			{
				SystemName = "STOCK",
				Text = "QUESTION_FINANCE_ACT_STOCK",
            };
			var qDebt = new Choice
			{
				SystemName = "DEBT",
				Text = "QUESTION_FINANCE_ACT_DEBT",

			};
			var qInvestment = new Choice
			{
				SystemName = "INVESTMENT",
				Text = "QUESTION_FINANCE_ACT_INVESTMENT",
                
			};

			var qFutureContract = new Choice
			{
				SystemName = "CONTRACT",
				Text = "QUESTION_FINANCE_ACT_CONTRACT",
            };
			var qFunds = new Choice
			{
				SystemName = "FUNDS",
				Text = "QUESTION_FINANCE_ACT_FUNDS",
			};

            var qStockSBL = new Choice
            {
                SystemName = "STOCK_SBL",
                Text = "QUESTION_FINANCE_ACT_STOCK_SBL",
            };

            qStock.ForbiddenChoices = new List<Guid>()
            {
                qFutureContract.ChoiceID,
            };

            qFutureContract.ForbiddenChoices = new List<Guid>()
            {
                qStock.ChoiceID,
            };

            qFinanceAct.Choices.AddRange(new Choice[]
			{   qConsultant,
				qStock,
				qDebt,
				qInvestment,
				qFutureContract,
				qFunds,
                qStockSBL
            });

			quizFinance.Questions.Add(qFinanceAct);

            /*
			// 2
			var qSecurities = new Question
			{
				SystemName = "QUESTION_FINANCE_SECURITIES",
				Text = "QUESTION_FINANCE_SECURITIES",

			};

			var qSecurities_yes = new Choice
			{
				SystemName = "QUESTION_FINANCE_SECURITIES_YES",
				Text = "QUESTION_FINANCE_SECURITIES_YES",
			};
			var qSecurities_no = new Choice
			{
				SystemName = "QUESTION_FINANCE_SECURITIES_NO",
				Text = "QUESTION_FINANCE_SECURITIES_NO",
			};

			qSecurities.Choices.AddRange(new Choice[]
			{   qSecurities_yes,
				qSecurities_no
			});
			quizFinance.Questions.Add(qSecurities);

			//3
			var qOtherAct = new Question
			{
				SystemName = "QUESTION_FINANCE_OTHER_ACT",
				Text = "QUESTION_FINANCE_OTHER_ACT"
			};

			var qOtherAct_yes = new Choice
			{
				SystemName = "QUESTION_FINANCE_OTHER_ACT_YES",
				Text = "QUESTION_FINANCE_OTHER_ACT",
			};
			var qOtherAct_no = new Choice
			{
				SystemName = "QUESTION_FINANCE_OTHER_ACT_NO",
				Text = "QUESTION_FINANCE_OTHER_ACT_NO",
			};

			qOtherAct.Choices.AddRange(new Choice[]
			{   qOtherAct_yes,
				qOtherAct_no
			});
			quizFinance.Questions.Add(qOtherAct);
            */

			#region Suggestions
			quizFinance.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามข้อมูลเพิ่มเติมได้ที่สํานักงานคณะกรรมการหลักทรัพย์และกํากับหลักทรัพย์ <a href='https://www.sec.or.th/TH/Pages/Home.aspx'>https://www.sec.or.th/TH/Pages/Home.aspx</a>  หรือ โทร 0-2033-9999",
                    RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition("return ($(\"input[name='QUESTION_FINANCE_ACT_CONSULTANT']\").prop('checked') && $(\"input[name='QUESTION_FINANCE_ACT_CONTRACT']\").prop('checked'))"),
						new SuggestionCondition("return ($(\"input[name='QUESTION_FINANCE_ACT_DEBT']\").prop('checked') && $(\"input[name='QUESTION_FINANCE_ACT_FUNDS']\").prop('checked'))"),
                        new SuggestionCondition("return $(\"input[name^='QUESTION_FINANCE_ACT']:checked\").length > 2;"),
                    },
				},
				//new Suggestion()
				//{
				//	Message = "กรุณาติดต่อสอบถามข้อมูลเพิ่มเติมได้ที่สํานักงานคณะกรรมการหลักทรัพย์และกํากับหลักทรัพย์ <a href='https://www.sec.or.th/TH/Pages/Home.aspx'>https://www.sec.or.th/TH/Pages/Home.aspx</a>  หรือ โทร 0-2033-9999",
                //  RequireAllCondition = true,
				//	Conditions = new SuggestionCondition[]
				//	{
				//		new SuggestionCondition("return $(\"input[name^='QUESTION_FINANCE_ACT']:checked\").length > 1;"),
				//	},
				//},
			};

			#endregion

			db.InsertOne(quizFinance);

			// ===========================================================
			// Quiz App Mapping Init
			// ===========================================================

			var dbAppMapping = MongoFactory.GetQuizAppMappingCollection();
			QuizAppMapping[] items = new QuizAppMapping[]
			{  
				//qConsultant
				new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_FINANCE_TYPE,
                    AppSystemName = AppSystemNameTextConst.APP_SEC_NEW_E,
                    DisplayConditions = new QuizAnswer[] {

                        new QuizAnswerGroup(true, new QuizAnswer[]
                        {
                        new QuizAnswer(qFinanceAct.SystemName+"_"+qConsultant.SystemName, "on"),

                        })
                    },
                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    },
                },
				//qStock
				new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_FINANCE_TYPE,  //ก
					AppSystemName = AppSystemNameTextConst.APP_SEC_NEW_A,
                    DisplayConditions = new QuizAnswer[] {

                        new QuizAnswer(qFinanceAct.SystemName+"_"+qStock.SystemName, "on"),
                    },

                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    },
                },

				//qDebt
				new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_FINANCE_TYPE,  //ข

					AppSystemName = AppSystemNameTextConst.APP_SEC_NEW_B,
                    DisplayConditions = new QuizAnswer[] {
                        new QuizAnswer(qFinanceAct.SystemName+"_"+qDebt.SystemName, "on"),
                    },

                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    },
                },

				//qInvestment
				new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_FINANCE_TYPE,  //ง
					AppSystemName = AppSystemNameTextConst.APP_SEC_NEW_D,
                    DisplayConditions = new QuizAnswer[] {

                        new QuizAnswer(qFinanceAct.SystemName+"_"+qInvestment.SystemName, "on"),
                    },
                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    },
                },

				//qFutureContract
				new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_FINANCE_TYPE, 
					AppSystemName = AppSystemNameTextConst.APP_SEC_NEW_G,
                    DisplayConditions = new QuizAnswer[] {

                        new QuizAnswer(qFinanceAct.SystemName+"_"+qFutureContract.SystemName, "on"),
                    },
                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    },
                },

				//qFunds
				new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_FINANCE_TYPE,  //ค
					AppSystemName = AppSystemNameTextConst.APP_SEC_NEW_C,
                    DisplayConditions = new QuizAnswer[] {

                        new QuizAnswer(qFinanceAct.SystemName+"_"+qFunds.SystemName, "on"),
                    },
                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    },
                },

                //qStockSBL
				new QuizAppMapping()
                {
                    QuizGroup = BUSINESS_FINANCE_TYPE, 
					AppSystemName = AppSystemNameTextConst.APP_SEC_NEW_F,
                    DisplayConditions = new QuizAnswer[] {

                        new QuizAnswer(qFinanceAct.SystemName+"_"+qStockSBL.SystemName, "on"),
                    },
                    OnlineRequestAllowedConditions = new QuizAnswer[]
                    {
                    },
                },

            };


            dbAppMapping.InsertMany(items);
        }
		#region Question Generators

		private static void AddSignTaxQuestion(SmartQuiz quiz, string businessType, Question parentQuestion = null, Choice preRequisiteChoice = null)
		{
			string prefix = "QUESTION_" + businessType;
			var qSignTax = new Question
			{
				SystemName = prefix + "_SIGN_TAX",
				Text = "QUESTION_COMMON_SIGN_TAX",
			};
			var qSignTax_yes = new Choice
			{
				SystemName = prefix + "_SIGN_TAX__YES",
				Text = "QUESTION_COMMON_SIGN_TAX__YES",
			};
			var qSignTax_no = new Choice
			{
				SystemName = prefix + "_SIGN_TAX__NO",
				Text = "QUESTION_COMMON_SIGN_TAX__NO",
			};
			qSignTax.Choices.AddRange(new Choice[]
			{
				qSignTax_yes,
				qSignTax_no,
			});

			if (parentQuestion != null && preRequisiteChoice != null)
			{
				parentQuestion.SubQuestions.Add(qSignTax);
				preRequisiteChoice.NextQuestionID = qSignTax.QuestionID.ToString();
			}
			else
			{
				quiz.Questions.Add(qSignTax);
			}

		}

		private static QuizAppMapping[] AddSignTaxAppMappings(QuizAppMapping[] mappings, string businessType, List<QuizAnswer> preRequisiteAnswers = null)
		{
			List<QuizAppMapping> newMapping = mappings.ToList();
			string prefix = "QUESTION_" + businessType;

			List<QuizAnswer> displayConditions = new List<QuizAnswer>() { new QuizAnswer(prefix + "_SIGN_TAX", prefix + "_SIGN_TAX__YES") };

			if (preRequisiteAnswers != null && preRequisiteAnswers.Count > 0)
				displayConditions.AddRange(preRequisiteAnswers);

			newMapping.Add(new QuizAppMapping()
			{
				QuizGroup = businessType,
				AppSystemName = "ยื่นชำระภาษีป้าย",
				DisplayConditions = displayConditions.ToArray(),
				OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
			});

			return newMapping.ToArray();
		}


		private static void AddShopConstructionQuestion(SmartQuiz quiz, string businessType)
		{
			string prefix = "QUESTION_" + businessType;

			var qConst = new Question
			{
				SystemName = prefix + "_CONSTRUCTION",
				Text = "QUESTION_COMMON_CONSTRUCTION",
				//Icon = "~/img/SmartQuiz/10 shop.svg",
			};
			var qConst_yes = new Choice
			{
				SystemName = prefix + "_CONSTRUCTION__YES",
				Text = "QUESTION_COMMON_CONSTRUCTION__YES",
			};
			var qConst_no = new Choice
			{
				SystemName = prefix + "_CONSTRUCTION__NO",
				Text = "QUESTION_COMMON_CONSTRUCTION__NO",
			};
			qConst.Choices.AddRange(new Choice[]
			{
				qConst_yes,
				qConst_no,
			});

			quiz.Questions.Add(qConst);

			// 1
			var qType = new Question
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE",
				Text = "QUESTION_CONSTRUCTION_TYPE",
				AllowMultipleAnswer = true
			};

			var qType_extraLarge = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__EXTRA_LARGE",
				Text = "QUESTION_CONSTRUCTION_TYPE__EXTRA_LARGE",
			};

			var qType_high = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__HIGH",
				Text = "QUESTION_CONSTRUCTION_TYPE__HIGH",
			};

			var qType_oil = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__OIL",
				Text = "QUESTION_CONSTRUCTION_TYPE__OIL",
			};

			var qType_public = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__PUBLIC",
				Text = "QUESTION_CONSTRUCTION_TYPE__PUBLIC",
			};

			var qType_none = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE__NONE",
				Text = "QUESTION_CONSTRUCTION_TYPE__NONE",
			};

			qType.Choices.AddRange(new Choice[]
			{
				qType_extraLarge,
				qType_high,
				qType_oil,
				qType_public,
				qType_none
			});

			qConst.SubQuestions.Add(qType);
			qConst_yes.NextQuestionID = qType.QuestionID.ToString();



			// 1.1
			var qIsLarge = new Question
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE",
				Text = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE",
			};
			var qIsLarge_yes = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__YES",
				Text = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__YES",
			};
			var qIsLarge_no = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__NO",
				Text = "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__NO",
			};

			qIsLarge.Choices.AddRange(new Choice[]
			{
				qIsLarge_yes,
				qIsLarge_no,
			});
			qType.SubQuestions.Add(qIsLarge);
			qType_none.NextQuestionID = qIsLarge.QuestionID.ToString();

			// 1.2
			var qOrgPlace = new Question
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE",
				Text = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE",
			};
			var qOrgPlace_bkk = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__BANGKOK",
				Text = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__BANGKOK",
			};
			var qOrgPlace_local = new Choice
			{
				SystemName = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__LOCAL",
				Text = "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__LOCAL",
			};

			qOrgPlace.Choices.AddRange(new Choice[]
			{
				qOrgPlace_bkk,
				qOrgPlace_local,
			});
			qIsLarge.SubQuestions.Add(qOrgPlace);
			qIsLarge_yes.NextQuestionID = qOrgPlace.QuestionID.ToString();

		}

		private static QuizAppMapping[] AddShopConstructionAppMappings(QuizAppMapping[] mappings, string businessType)
		{
			List<QuizAppMapping> newMapping = mappings.ToList();
			string prefix = "QUESTION_" + businessType;

			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = businessType,
					AppSystemName = "ใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีพิเศษ",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
							new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_QUESTION_CONSTRUCTION_TYPE__EXTRA_LARGE", "on"),
							new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_QUESTION_CONSTRUCTION_TYPE__HIGH", "on"),
							new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_QUESTION_CONSTRUCTION_TYPE__OIL", "on"),
							new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_QUESTION_CONSTRUCTION_TYPE__PUBLIC", "on"),
						})

					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = businessType,
					AppSystemName = "ใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอื่นๆ",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_QUESTION_CONSTRUCTION_TYPE__NONE", "on"),
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_IS_LARGE", "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__NO"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = businessType,
					AppSystemName = "ขอใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักการโยธา",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_QUESTION_CONSTRUCTION_TYPE__NONE", "on"),
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_IS_LARGE", "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__YES"),
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_ORG_PLACE", "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__BANGKOK"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				new QuizAppMapping()
				{
					QuizGroup = businessType,
					AppSystemName = "ขอใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักงานเขต",
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_QUESTION_CONSTRUCTION_TYPE__NONE", "on"),
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_IS_LARGE", "QUESTION_CONSTRUCTION_TYPE_IS_LARGE__YES"),
						new QuizAnswer("QUESTION_CONSTRUCTION_TYPE_ORG_PLACE", "QUESTION_CONSTRUCTION_TYPE_ORG_PLACE__LOCAL"),
					},
					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
			};

			newMapping.AddRange(items.ToList());

			return newMapping.ToArray();
		}
		private static void AddClothesQuestion(SmartQuiz quiz, string businessType)
		{
			string prefix = "QUESTION_" + businessType;

			// 1
			var qPrivate = new Question
			{
				SystemName = prefix + "_PRIVATE",
				Text = "QUESTION_COMMON_PRIVATE",

			};

			// 1
			var qBuySell = new Question
			{
				SystemName = prefix + "_BUYSELL",
				Text = "QUESTION_COMMON_BUYSELL",
			};
			var qBuySell_yes = new Choice
			{
				SystemName = prefix + "_BUYSELL_YES",
				Text = "QUESTION_COMMON_BUYSELL_YES",
			};
			var qBuySell_no = new Choice
			{
				SystemName = prefix + "_BUYSELL_NO",
				Text = "QUESTION_COMMON_BUYSELL_NO",
			};

			qBuySell.Choices.AddRange(new Choice[]
			{   qBuySell_yes,
				qBuySell_no
			});
			quiz.Questions.Add(qBuySell);
			// 1.1
			var qSaleRoutine = new Question
			{
				SystemName = prefix + "_SALE_ROUTINE",
				Text = "QUESTION_COMMON_SALE_ROUTINE",

			};

			var qSaleRoutine_yes = new Choice
			{
				SystemName = prefix + "_SALE_ROUTINE_YES",
				Text = "QUESTION_COMMON_SALE_ROUTINE_YES",
			};
			var qSaleRoutine_no = new Choice
			{
				SystemName = prefix + "_SALE_ROUTINE_NO",
				Text = "QUESTION_COMMON_SALE_ROUTINE_NO",
			};

			qSaleRoutine.Choices.AddRange(new Choice[]
			{   qSaleRoutine_yes,
				qSaleRoutine_no
			});
			qBuySell.SubQuestions.Add(qSaleRoutine);
			qBuySell_yes.NextQuestionID = qSaleRoutine.QuestionID.ToString();

			//1.1.1
			var qLocation = new Question
			{
				SystemName = prefix + "_LOCATION",
				Text = "QUESTION_COMMON_LOCATION",

			};

			var qLocation_yes = new Choice
			{
				SystemName = prefix + "_LOCATION_YES",
				Text = "QUESTION_COMMON_LOCATION_YES",
			};
			var qLocation_no = new Choice
			{
				SystemName = prefix + "_LOCATION_NO",
				Text = "QUESTION_COMMON_LOCATION_NO",
			};

			qLocation.Choices.AddRange(new Choice[]
			{   qLocation_yes,
				qLocation_no
			});
			qSaleRoutine.SubQuestions.Add(qLocation);
			qSaleRoutine_yes.NextQuestionID = qLocation.QuestionID.ToString();


			//1.1.2
			var qSaleOnline = new Question
			{
				SystemName = prefix + "_SALE_ONLINE",
				Text = "QUESTION_COMMON_SALE_ONLINE",

			};

			var qSaleOnline_yes = new Choice
			{
				SystemName = prefix + "_SALE_ONLINE_YES",
				Text = "QUESTION_COMMON_SALE_ONLINE_YES",
			};
			var qSaleOnline_no = new Choice
			{
				SystemName = prefix + "_SALE_ONLINE_NO",
				Text = "QUESTION_COMMON_SALE_ONLINE_NO",
			};

			qSaleOnline.Choices.AddRange(new Choice[]
			{   qSaleOnline_yes,
				qSaleOnline_no
			});
			qLocation.SubQuestions.Add(qSaleOnline);
			qLocation_yes.NextQuestionID = qSaleOnline.QuestionID.ToString();

			//1.1.2.1
			var qOrder = new Question
			{
				SystemName = prefix + "_ORDER",
				Text = "QUESTION_COMMON_ORDER",
				AllowMultipleAnswer = true,

			};

			var qShoppingCart = new Choice
			{
				SystemName = "SHOPPINGCART",
				Text = "QUESTION_COMMON_ORDER_SHOPPINGCART",
			};
			var qForm = new Choice
			{
				SystemName = "FORM",
				Text = "QUESTION_COMMON_ORDER_FORM",
			};
			var qEmail = new Choice
			{
				SystemName = "EMAIL",
				Text = "QUESTION_COMMON_ORDER_EMAIL",
			};
			var qPhone = new Choice
			{
				SystemName = "PHONE",
				Text = "QUESTION_COMMON_ORDER_PHONE",
			};
			var qFax = new Choice
			{
				SystemName = "FAX",
				Text = "QUESTION_COMMON_ORDER_FAX",
			};
			var qOrder_Other = new Choice
			{
				SystemName = "OTHER",
				Text = "QUESTION_COMMON_ORDER_OTHER",
			};
			var qOrder_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_COMMON_ORDER_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qShoppingCart.ChoiceID,
					qForm.ChoiceID,
					qEmail.ChoiceID,
					qPhone.ChoiceID,
					qFax.ChoiceID,
					qOrder_Other.ChoiceID,
				}
			};
			qOrder.Choices.AddRange(new Choice[]
			{   qShoppingCart,
				qForm,
				qEmail,
				qPhone,
				qFax,
				qOrder_Other,
				qOrder_no
			});
			qSaleOnline.SubQuestions.Add(qOrder);
			qSaleOnline_yes.NextQuestionID = qOrder.QuestionID.ToString();
			//1.1.2.1.1
			var qPay = new Question
			{
				SystemName = prefix + "_PAY",
				Text = "QUESTION_COMMON_PAY",
				AllowMultipleAnswer = true,

			};

			var qOffline = new Choice
			{
				SystemName = "OFFLINE",
				Text = "QUESTION_COMMON_PAY_OFFLINE",
			};
			var qCredit = new Choice
			{
				SystemName = "CREDIT",
				Text = "QUESTION_COMMON_PAY_CREDIT",
			};
			var qEbanking = new Choice
			{
				SystemName = "EBANKING",
				Text = "QUESTION_COMMON_PAY_EBANKING",
			};
			var qPayPal = new Choice
			{
				SystemName = "PAYPAL",
				Text = "QUESTION_COMMON_PAY_PAYPAL",
			};
			var qPayOther = new Choice
			{
				SystemName = "PAYOTHER",
				Text = "QUESTION_COMMON_PAY_PAYOTHER",
			};
			var qPay_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_COMMON_PAY_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qOffline.ChoiceID,
					qCredit.ChoiceID,
					qEbanking.ChoiceID,
					qPayPal.ChoiceID,
					qPayOther.ChoiceID,
				}
			};
			qPay.Choices.AddRange(new Choice[]
			{   qOffline,
				qCredit,
				qEbanking,
				qPayPal,
				qPayOther,
				qPay_no
			});
			qOrder.SubQuestions.Add(qPay);
			qShoppingCart.NextQuestionID = qPay.QuestionID.ToString();
			qForm.NextQuestionID = qPay.QuestionID.ToString();
			qEmail.NextQuestionID = qPay.QuestionID.ToString();
			qPhone.NextQuestionID = qPay.QuestionID.ToString();
			qFax.NextQuestionID = qPay.QuestionID.ToString();
			qOrder_Other.NextQuestionID = qPay.QuestionID.ToString();

			//1.1.2.1.1.1
			var qChannel = new Question
			{
				SystemName = prefix + "_CHANNEL",
				Text = "QUESTION_COMMON_CHANNEL",
				AllowMultipleAnswer = true,

			};
			var qTranspot = new Choice
			{
				SystemName = "TRANSPOT",
				Text = "QUESTION_COMMON_CHANNEL_TRANSPOT",
			};
			var qPost = new Choice
			{
				SystemName = "POST",
				Text = "QUESTION_COMMON_CHANNEL_POST",
			};
			var qDelivery = new Choice
			{
				SystemName = "DELIVERY",
				Text = "QUESTION_COMMON_CHANNEL_DELIVERY",
			};
			var qDownload = new Choice
			{
				SystemName = "DOWNLOAD",
				Text = "QUESTION_COMMON_CHANNEL_DOWNLOAD",
			};
			var qChannel_Email = new Choice
			{
				SystemName = "EMAIL",
				Text = "QUESTION_COMMON_CHANNEL_EMAIL",
			};
			var qChannel_Other = new Choice
			{
				SystemName = "OTHER",
				Text = "QUESTION_COMMON_CHANNEL_OTHER",
			};
			var qChannel_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_COMMON_CHANNEL_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qTranspot.ChoiceID,
					qPost.ChoiceID,
					qDelivery.ChoiceID,
					qDownload.ChoiceID,
					qChannel_Email.ChoiceID,
					qChannel_Other.ChoiceID,
				}
			};
			qChannel.Choices.AddRange(new Choice[]
			{       qTranspot,
					qPost,
					qDelivery,
					qDownload,
					qChannel_Email,
					qChannel_Other,
					qChannel_no
			});
			qPay.SubQuestions.Add(qChannel);
			qOffline.NextQuestionID = qChannel.QuestionID.ToString();
			qCredit.NextQuestionID = qChannel.QuestionID.ToString();
			qEbanking.NextQuestionID = qChannel.QuestionID.ToString();
			qPayPal.NextQuestionID = qChannel.QuestionID.ToString();
			qPayOther.NextQuestionID = qChannel.QuestionID.ToString();


			#region Suggestions
			quiz.Suggestions = new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามข้อมูลเพิ่มเติมได้ที่กรมพัฒนาธุรกิจการค้า <a href='https://www.dbd.go.th/index.php'> https://www.dbd.go.th/index.php </a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qSaleRoutine.SystemName, new string[] { qSaleRoutine_no.SystemName }),
						new SuggestionCondition(qOrder.SystemName+"_"+qOrder_no.SystemName, new string[] { "true" }),
						new SuggestionCondition(qPay.SystemName+"_"+qPay_no.SystemName, new string[] { "true" }),
						new SuggestionCondition(qChannel.SystemName+"_"+qChannel_no.SystemName, new string[] { "true" }),
					}
				},
				new Suggestion()
				{
					Message = "กรุณาศึกษาข้อมูลเพิ่มเติมผู้ที่ต้องจดทะเบียนพาณิชย์ <a href='https://www.trustmarkthai.com/media/k2/attachments/form003.pdf'> https://www.trustmarkthai.com/media/k2/attachments/form003.pdf </a>",
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qBuySell.SystemName, new string[] { qBuySell_no.SystemName }),
					}
				},
				new Suggestion()
				{
					Message = "กรุณาศึกษาข้อมูลเพิ่มเติมการจดทะเบียนพาณิชย์สําหรับธุรกิจพาณิชย์อิเล็กทรอนิกส์ <a href='https://www.trustmarkthai.com/media/k2/attachments/Register_Por_KO_0403.pdf'> Register_Por_KO_0403.pdf </a>",
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qSaleOnline.SystemName, new string[] { qSaleOnline_no.SystemName }),
					}
				},
			};

			#endregion


		}

		private static void AddHospitalQuestion(SmartQuiz quiz, string businessType)
		{
			string prefix = "QUESTION_" + businessType;

			// 1
			var qPrivate = new Question
			{
				SystemName = prefix + "_PRIVATE",
				Text = "QUESTION_COMMON_PRIVATE",

			};

			var qPrivate_yes = new Choice
			{
				SystemName = prefix + "_PRIVATE_YES",
				Text = "QUESTION_COMMON_PRIVATE_YES",
			};
			var qPrivate_no = new Choice
			{
				SystemName = prefix + "_PRIVATE_NO",
				Text = "QUESTION_COMMON_PRIVATE_NO",
			};

			qPrivate.Choices.AddRange(new Choice[]
			{
				qPrivate_yes,
				qPrivate_no,
			});
			quiz.Questions.Add(qPrivate);

			//1.1
			var qDoctor = new Question
			{
				SystemName = prefix + "_DOCTOR",
				Text = "QUESTION_COMMON_DOCTOR",


			};
			var qDoctor_yes = new Choice
			{
				SystemName = prefix + "_DOCTOR_YES",
				Text = "QUESTION_COMMON_DOCTOR_YES",

			};
			var qDoctor_no = new Choice
			{
				SystemName = prefix + "_DOCTOR_NO",
				Text = "QUESTION_COMMON_DOCTOR_NO",

			};

			qDoctor.Choices.AddRange(new Choice[]
			{
				qDoctor_yes,
				qDoctor_no
			});
			qPrivate.SubQuestions.Add(qDoctor);
			qPrivate_yes.NextQuestionID = qDoctor.QuestionID.ToString();

			//1.1.1
			var qType = new Question
			{
				SystemName = prefix + "_TYPE",
				Text = "QUESTION_COMMON_TYPE",
			};
			var qType_Hospital = new Choice
			{
				SystemName = prefix + "_TYPE_HOSPITAL",
				Text = "QUESTION_COMMON_TYPE_HOSPITAL",
			};
			var qType_Clinic = new Choice
			{
				SystemName = prefix + "_TYPE_CLINIC",
				Text = "QUESTION_COMMON_TYPE_CLINIC",
			};
			qType.Choices.AddRange(new Choice[]
			{
				qType_Hospital,
				qType_Clinic
			});
			qDoctor.SubQuestions.Add(qType);
			qDoctor_yes.NextQuestionID = qType.QuestionID.ToString();

			//1.1.1.1
			var qRequest_ApprovalPlan = new Question
			{
				SystemName = prefix + "_APPROVAL_PLAN",
				Text = "QUESTION_COMMON_APPROVAL_PLAN",
			};
			var qRequest_ApprovalPlan_yes = new Choice
			{
				SystemName = prefix + "_APPROVAL_PLAN_YES",
				Text = "QUESTION_COMMON_APPROVAL_PLAN_YES",

			};
			var qRequest_ApprovalPlan_no = new Choice
			{
				SystemName = prefix + "_APPROVAL_PLAN_NO",
				Text = "QUESTION_COMMON_APPROVAL_PLAN_NO",
			};
			qRequest_ApprovalPlan.Choices.AddRange(new Choice[]
			{
				qRequest_ApprovalPlan_yes,
				qRequest_ApprovalPlan_no
			});
			qType.SubQuestions.Add(qRequest_ApprovalPlan);
			qType_Hospital.NextQuestionID = qRequest_ApprovalPlan.QuestionID.ToString();

            #region Suggestions
            if (quiz.Suggestions == null) quiz.Suggestions = new Suggestion[] { };

            quiz.Suggestions = quiz.Suggestions.Concat(new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาติดต่อสอบถามเพิ่มเติมได้ที่กรมสนับสนุนบริการสุขภาพ <a href='http://hss.moph.go.th/'>http://hss.moph.go.th/</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{

						new SuggestionCondition(qPrivate.SystemName, new string[] { qPrivate_no.SystemName }),
						new SuggestionCondition(qDoctor.SystemName, new string[] { qDoctor_no.SystemName }),
					}
				},
			}).ToArray();

			#endregion
		}
		private static void AddMedicalToolsQuestion(SmartQuiz quiz, string businessType)
		{
			string prefix = "QUESTION_" + businessType;


			// 1
			var qImport = new Question
			{
				SystemName = prefix + "_IMPORT",
				Text = "QUESTION_COMMON_IMPORT",
			};
			var qImport_yes = new Choice
			{
				SystemName = prefix + "_IMPORT_YES",
				Text = "QUESTION_COMMON_IMPORT_YES",
			};
			var qImport_no = new Choice
			{
				SystemName = prefix + "_IMPORT_NO",
				Text = "QUESTION_COMMON_IMPORT_NO",
			};

			qImport.Choices.AddRange(new Choice[]
			{
				qImport_yes,
				qImport_no,
			});
			quiz.Questions.Add(qImport);

			//1.1
			var qProductTarget = new Question
			{
				SystemName = prefix + "_PRODUCT_TARGET",
				Text = "QUESTION_COMMON_PRODUCT_TARGET",
				Info = "QUESTION_COMMON_PRODUCT_TARGET_INFO",
				AllowMultipleAnswer = true,
			};
			var qProductTarget_A = new Choice
			{
				SystemName = "A",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_A",

			};
			var qProductTarget_B = new Choice
			{
				SystemName = "B",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_B",

			};
			var qProductTarget_C = new Choice
			{
				SystemName = "C",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_C",

			};
			var qProductTarget_D = new Choice
			{
				SystemName = "D",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_D",

			};
			var qProductTarget_E = new Choice
			{
				SystemName = "E",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_E",

			};
			var qProductTarget_F = new Choice
			{
				SystemName = "F",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_F",

			};
			var qProductTarget_G = new Choice
			{
				SystemName = "G",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_G",

			};
			var qProductTarget_H = new Choice
			{
				SystemName = "H",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_H",

			};
			var qProductTarget_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_COMMON_PRODUCT_TARGET_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qProductTarget_A.ChoiceID,
					qProductTarget_B.ChoiceID,
					qProductTarget_C.ChoiceID,
					qProductTarget_D.ChoiceID,
					qProductTarget_E.ChoiceID,
					qProductTarget_F.ChoiceID,
					qProductTarget_G.ChoiceID,
					qProductTarget_H.ChoiceID,
				}

			};
			qProductTarget.Choices.AddRange(new Choice[]
			{
				qProductTarget_A,
				qProductTarget_B,
				qProductTarget_C,
				qProductTarget_D,
				qProductTarget_E,
				qProductTarget_F,
				qProductTarget_G,
				qProductTarget_H,
				qProductTarget_no,
			});
			qImport.SubQuestions.Add(qProductTarget);
			qImport_yes.NextQuestionID = qProductTarget.QuestionID.ToString();

			//1.1.1
			var qAchievement = new Question
			{
				SystemName = prefix + "_ACHIEVEMENT",
				Text = "QUESTION_COMMON_ACHIEVEMENT",
			};
			var qAchievement_yes = new Choice
			{
				SystemName = prefix + "_ACHIEVEMENT_YES",
				Text = "QUESTION_COMMON_ACHIEVEMENT_YES",
			};
			var qAchievement_no = new Choice
			{
				SystemName = prefix + "_ACHIEVEMENT_NO",
				Text = "QUESTION_COMMON_ACHIEVEMENT_NO",
			};
			qAchievement.Choices.AddRange(new Choice[]
			{
				qAchievement_yes,
				qAchievement_no
			});
			qProductTarget.SubQuestions.Add(qAchievement);
			qProductTarget_A.NextQuestionID = qAchievement.QuestionID.ToString();
			qProductTarget_B.NextQuestionID = qAchievement.QuestionID.ToString();
			qProductTarget_C.NextQuestionID = qAchievement.QuestionID.ToString();
			qProductTarget_D.NextQuestionID = qAchievement.QuestionID.ToString();
			qProductTarget_E.NextQuestionID = qAchievement.QuestionID.ToString();
			qProductTarget_F.NextQuestionID = qAchievement.QuestionID.ToString();
			qProductTarget_G.NextQuestionID = qAchievement.QuestionID.ToString();
			qProductTarget_H.NextQuestionID = qAchievement.QuestionID.ToString();

			//1.1.1.1
			var qEquipment = new Question
			{
				SystemName = prefix + "_EQUIPMENT",
				Text = "QUESTION_COMMON_EQUIPMENT",
				AllowMultipleAnswer = true
			};
			var qEquipment_Condom = new Choice
			{
				SystemName = "CONDOM",
				Text = "QUESTION_COMMON_EQUIPMENT_CONDOM",
			};
			var qEquipment_ContactLens = new Choice
			{
				SystemName = "CONTACTLENS",
				Text = "QUESTION_COMMON_EQUIPMENT_CONTACTLENS",
			};
			var qEquipment_Gloves = new Choice
			{
				SystemName = "GLOVES",
				Text = "QUESTION_COMMON_EQUIPMENT_GLOVES",
			};
			var qEquipment_BloodBag = new Choice
			{
				SystemName = "BLOODBAG",
				Text = "QUESTION_COMMON_EQUIPMENT_BLOODBAG",
			};
			var qEquipment_HIVTest = new Choice
			{
				SystemName = "HIVTEST",
				Text = "QUESTION_COMMON_EQUIPMENT_HIVTEST",
			};
			var qEquipment_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_COMMON_EQUIPMENT_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qEquipment_Condom.ChoiceID,
					qEquipment_ContactLens.ChoiceID,
					qEquipment_Gloves.ChoiceID,
					qEquipment_BloodBag.ChoiceID,
					qEquipment_HIVTest.ChoiceID,
				}
			};
			qEquipment.Choices.AddRange(new Choice[]
			{
				qEquipment_Condom,
				qEquipment_ContactLens,
				qEquipment_Gloves,
				qEquipment_BloodBag,
				qEquipment_HIVTest,
				qEquipment_no
			});
			qAchievement.SubQuestions.Add(qEquipment);
			qAchievement_no.NextQuestionID = qEquipment.QuestionID.ToString();
			//1.1.1.1.1
			var qEquipment2 = new Question
			{
				SystemName = prefix + "_EQUIPMENT2",
				Text = "QUESTION_COMMON_EQUIPMENT",
				AllowMultipleAnswer = true
			};
			var qEquipment2_PhysicalTherapy = new Choice
			{
				SystemName = "PHYSICAL_THERAPY",
				Text = "QUESTION_COMMON_EQUIPMENT2_PHYSICAL_THERAPY",
			};
			var qEquipment2_Alcohol = new Choice
			{
				SystemName = "ALCOHOL",
				Text = "QUESTION_COMMON_EQUIPMENT2_ALCOHOL",
			};
			var qEquipment2_TeethWhitening = new Choice
			{
				SystemName = "TEETH_WHITENING",
				Text = "QUESTION_COMMON_EQUIPMENT2_TEETH_WHITENING",
			};
			var qEquipment2_ContactLensCare = new Choice
			{
				SystemName = "CONTACTLENS_CARE",
				Text = "QUESTION_COMMON_EQUIPMENT2_CONTACTLENS_CARE",
			};
			var qEquipment2_Silicone = new Choice
			{
				SystemName = "SILICONE",
				Text = "QUESTION_COMMON_EQUIPMENT2_SILICONE",
			};
			var qEquipment2_TightenBreast = new Choice
			{
				SystemName = "TIGHTEN_BREAST",
				Text = "QUESTION_COMMON_EQUIPMENT2_TIGHTEN_BREAST",
			};
			var qEquipment2_DrugTest = new Choice
			{
				SystemName = "DRUGTEST",
				Text = "QUESTION_COMMON_EQUIPMENT2_DRUGTEST",
			};
			var qEquipment2_EyeSurgery = new Choice
			{
				SystemName = "EYE_SURGERY",
				Text = "QUESTION_COMMON_EQUIPMENT2_EYE_SURGERY",
			};
			var qEquipment2_KidneyCleanser = new Choice
			{
				SystemName = "KIDNEY_CLENSER",
				Text = "QUESTION_COMMON_EQUIPMENT2_KIDNEY_CLENSER",
			};
			var qEquipment2_Insulin = new Choice
			{
				SystemName = "INSULIN",
				Text = "QUESTION_COMMON_EQUIPMENT2_INSULIN",
			};
			var qEquipment2_no = new Choice
			{
				SystemName = "NO",
				Text = "QUESTION_COMMON_EQUIPMENT2_NO",
				ForbiddenChoices = new List<Guid>()
				{
					qEquipment2_PhysicalTherapy.ChoiceID,
					qEquipment2_Alcohol.ChoiceID,
					qEquipment2_TeethWhitening.ChoiceID,
					qEquipment2_ContactLensCare.ChoiceID,
					qEquipment2_Silicone.ChoiceID,
					qEquipment2_TightenBreast.ChoiceID,
					qEquipment2_DrugTest.ChoiceID,
					qEquipment2_EyeSurgery.ChoiceID,
					qEquipment2_KidneyCleanser.ChoiceID,
					qEquipment2_Insulin.ChoiceID,
				}
			};
			qEquipment2.Choices.AddRange(new Choice[]
			{
				qEquipment2_PhysicalTherapy,
				qEquipment2_Alcohol,
				qEquipment2_TeethWhitening,
				qEquipment2_ContactLensCare,
				qEquipment2_Silicone,
				qEquipment2_TightenBreast,
				qEquipment2_DrugTest,
				qEquipment2_EyeSurgery,
				qEquipment2_KidneyCleanser,
				qEquipment2_Insulin,
				qEquipment2_no
			});

			qEquipment.SubQuestions.Add(qEquipment2);
			qEquipment_no.NextQuestionID = qEquipment2.QuestionID.ToString();


            #region Suggestions
            if (quiz.Suggestions == null) quiz.Suggestions = new Suggestion[] { };

            quiz.Suggestions = quiz.Suggestions.Concat(new Suggestion[]
			{
				new Suggestion()
				{
					Message = "กรุณาศึกษานิยามเครื่องมือแพทย์เพิ่มเติมได้ที่ กองควบคุมเครื่องมือแพทย์ สำนักงานคณะกรรมการอาหารและยา <a href='http://web.krisdika.go.th/lawHeadContent.jsp?fromPage=lawHeadContent&formatFile=htm&hID=0'>http://web.krisdika.go.th/lawHeadContent.jsp?fromPage=lawHeadContent&formatFile=htm&hID=0</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qProductTarget.SystemName+"_"+qProductTarget_no.SystemName, new string[] { "true" }),
					}
				},
                new Suggestion()
                {
                    Message = "กรุณาศึกษานิยามเครื่องมือแพทย์เพิ่มเติมได้ที่ กองควบคุมเครื่องมือแพทย์ สำนักงานคณะกรรมการอาหารและยา [<a href='http://web.krisdika.go.th/lawHeadContent.jsp?fromPage=lawHeadContent&formatFile=htm&hID=0'>http://web.krisdika.go.th/lawHeadContent.jsp?fromPage=lawHeadContent&formatFile=htm&hID=0</a>] หรือ ยื่นคำขอวินิจฉัยที่กองเครื่องมือแพทย์ โทร 02-590-7250 คู่มือประชาชน วินิจฉัย ผลิตภัณฑ์เครื่องมือแพทย์  [<a href='https://www.info.go.th/#!/th/search/4879/%E0%B8%A7%E0%B8%B4%E0%B8%99%E0%B8%B4%E0%B8%88%E0%B8%89%E0%B8%B1%E0%B8%A2%E0%B9%80%E0%B8%84%E0%B8%A3%E0%B8%B7%E0%B9%88%E0%B8%AD%E0%B8%87%E0%B8%A1%E0%B8%B7%E0%B8%AD%E0%B9%81%E0%B8%9E%E0%B8%97%E0%B8%A2%E0%B9%8C/'>https://www.info.go.th/#!/th/search/4879/วินิจฉัยเครื่องมือแพทย์/</a>]",
                    RequireAllCondition = false,
                    Conditions = new SuggestionCondition[]
                    {                        
                        new SuggestionCondition(qAchievement.SystemName, new string[] { qAchievement_yes.SystemName }),
                    }
                },
                new Suggestion()
				{
					Message = "กรุณาศึกษาข้อมูลเพิ่มเติมได้ที่ กองควบคุมเครื่องมือแพทย์ สำนักงานคณะกรรมการอาหารและยา <a href='http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx'>http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qImport.SystemName, new string[] { qImport_no.SystemName }),
					}
				},
				new Suggestion()
				{
					Message = "กรุณาติดต่อ อย. เพื่อยื่นคำขอจดทะเบียนสถานประกอบการนําเข้าเครื่องมือแพทย์ และคําขออนุญาตนําเข้าเครื่องมือแพทย์ จากเว็ปไซต์ <a href='http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx'>http://www.fda.moph.go.th/sites/Medical/SitePages/Relative.aspx</a>",
					RequireAllCondition = false,
					Conditions = new SuggestionCondition[]
					{
						new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_Condom.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_ContactLens.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_Gloves.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_BloodBag.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment.SystemName+"_"+qEquipment_HIVTest.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_PhysicalTherapy.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_Alcohol.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_TeethWhitening.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_ContactLensCare.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_Silicone.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_TightenBreast.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_DrugTest.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_EyeSurgery.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_KidneyCleanser.SystemName, new string[] { "true" }),
						new SuggestionCondition(qEquipment2.SystemName+"_"+qEquipment2_Insulin.SystemName, new string[] { "true" }),
					}
				},
			}).ToArray();
			#endregion

		}
		private static QuizAppMapping[] AddHospitalAppMappings(QuizAppMapping[] mappings, string businessType)
		{

			List<QuizAppMapping> newMapping = mappings.ToList();
			string prefix = "QUESTION_" + businessType;
			QuizAppMapping[] items = new QuizAppMapping[]
			{
			new QuizAppMapping()
			{
				QuizGroup = businessType,
				AppSystemName = AppSystemNameTextConst.APP_HOSPITAL,
				DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(prefix + "_APPROVAL_PLAN", prefix + "_APPROVAL_PLAN_NO")
					},

				OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
			},
			//new QuizAppMapping()
			//{
			//	QuizGroup = businessType,
			//	AppSystemName = AppSystemNameTextConst.APP_CLINIC,
			//	DisplayConditions = new QuizAnswer[] {
			//			new QuizAnswer(prefix + "_TYPE", prefix + "_TYPE_CLINIC")
			//	},
			//	OnlineRequestAllowedConditions = new QuizAnswer[]
			//		{
			//		}
			//},
			new QuizAppMapping()
			{
				QuizGroup = businessType,
				AppSystemName = AppSystemNameTextConst.APP_CLINIC_BUSINESS,
				DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(prefix + "_TYPE", prefix + "_TYPE_CLINIC")
				},
				OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
			},
			new QuizAppMapping()
			{
				QuizGroup = businessType,
				AppSystemName = AppSystemNameTextConst.APP_CLINIC_OPERATION,
				DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(prefix + "_TYPE", prefix + "_TYPE_CLINIC")
				},
				OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
			},
			//new QuizAppMapping()
			//{
			//		QuizGroup = businessType,
						
			//		AppSystemName = AppSystemNameTextConst.APP_HOSPITAL_PERMISSION,
			//		DisplayConditions = new QuizAnswer[] {
			//			new QuizAnswer(prefix + "_APPROVAL_PLAN", prefix + "_APPROVAL_PLAN_YES")
			//		},

			//		OnlineRequestAllowedConditions = new QuizAnswer[]
			//		{
			//		}
			//}
			new QuizAppMapping()
			{
					QuizGroup = businessType,

					AppSystemName = AppSystemNameTextConst.APP_HOSPITAL_BUSINESS,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(prefix + "_APPROVAL_PLAN", prefix + "_APPROVAL_PLAN_YES")
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
			},
			new QuizAppMapping()
			{
					QuizGroup = businessType,

					AppSystemName = AppSystemNameTextConst.APP_HOSPITAL_OPERATION,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(prefix + "_APPROVAL_PLAN", prefix + "_APPROVAL_PLAN_YES")
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
			}
			};

			newMapping.AddRange(items.ToList());
			return newMapping.ToArray();

		}
		private static QuizAppMapping[] AddMedicalToolsAppMappings(QuizAppMapping[] mappings, string businessType)
		{

			List<QuizAppMapping> newMapping = mappings.ToList();
			string prefix = "QUESTION_" + businessType;

			QuizAppMapping[] items = new QuizAppMapping[]
			{
				new QuizAppMapping()
				{
					QuizGroup = businessType,
					AppSystemName =  AppSystemNameTextConst.APP_IMPORT_MEDICAL_EQUIPMENT,

					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(prefix + "_EQUIPMENT2_"+"NO","on" )
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},

			};
			newMapping.AddRange(items.ToList());
			return newMapping.ToArray();

		}
		private static QuizAppMapping[] AddClothesAppMappings(QuizAppMapping[] mappings, string businessType)
		{

			List<QuizAppMapping> newMapping = mappings.ToList();
			string prefix = "QUESTION_" + businessType;
			QuizAppMapping[] items = new QuizAppMapping[]
			{

			new QuizAppMapping()
				{
					QuizGroup = businessType,

					AppSystemName = AppSystemNameTextConst.APP_SELL_PRODUCT_IN_PUBLIC_AREA,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswer(prefix + "_LOCATION",prefix + "_LOCATION_NO"),
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				},
				//new QuizAppMapping()
				//{
				//	QuizGroup = businessType,

				//	AppSystemName = AppSystemNameTextConst.APP_COMMERCIAL,    //"จดทะเบียนพาณิชย์",

				//	DisplayConditions = new QuizAnswer[] {
				//		new QuizAnswer(prefix + "_LOCATION",prefix + "_LOCATION_YES"),
				//	},

				//	OnlineRequestAllowedConditions = new QuizAnswer[]
				//	{
				//	}
				//},
				new QuizAppMapping()
				{
					QuizGroup = businessType,
					AppSystemName = AppSystemNameTextConst.APP_ELECTRONIC_COMMERCIAL,
					DisplayConditions = new QuizAnswer[] {
						new QuizAnswerGroup(true, new QuizAnswer[]
						{
						new QuizAnswer(prefix + "_CHANNEL_TRANSPOT", "on"),
						new QuizAnswer(prefix + "_CHANNEL_POST", "on"),
						new QuizAnswer(prefix + "_CHANNEL_DELIVERY", "on"),
						new QuizAnswer(prefix + "_CHANNEL_DOWNLOAD", "on"),
						new QuizAnswer(prefix + "_CHANNEL_EMAIL", "on"),
						new QuizAnswer(prefix + "_CHANNEL_OTHER", "on"),
						})
					},

					OnlineRequestAllowedConditions = new QuizAnswer[]
					{
					}
				 },
			};

			newMapping.AddRange(items.ToList());
			return newMapping.ToArray();

		}

		#endregion

		public string Text { get; set; }
		public string BusinessType { get; set; }
		public Suggestion[] Suggestions { get; set; }

		public List<Question> Questions { get; set; }

		public SmartQuiz(string businessType)
		{
			BusinessType = businessType;
			Questions = new List<Question>();
		}

		public static SmartQuiz GetSmartQuiz(string businessType)
		{
			var db = MongoFactory.GetSmartQuizCollection().AsQueryable();
			var smartQuiz = db.Where(o => o.BusinessType == businessType).SingleOrDefault();
			return smartQuiz;
		}

		public class Question
		{
			public Guid QuestionID { get; set; }

			public string SystemName { get; set; }

			public string Text { get; set; }
			public List<Choice> Choices { get; set; }

            public bool DefaultShowInfo { get; set; } = false;
			public string Info { get; set; }

			public Guid NextQuestionID { get; set; }

			public bool AllowMultipleAnswer { get; set; }

			public List<Question> SubQuestions { get; set; }

			public List<Guid> SelectedChoices { get; set; }

			public string Icon { get; set; }

			public Question()
			{
				QuestionID = Guid.NewGuid();
				SubQuestions = new List<Question>();
				Choices = new List<Choice>();
				SelectedChoices = new List<Guid>();
			}
		}
		public class Choice
		{
			public Guid ChoiceID { get; set; }
			public string SystemName { get; set; }
			public string Text { get; set; }

			public string NextQuestionID { get; set; }

			public List<Guid> RecommendedPermit { get; set; }

			public string SpecialAction { get; set; }

			public string Info { get; set; }

			public List<Guid> ForbiddenChoices { get; set; }

			public string ForbiddenInfo { get; set; }

			public Choice()
			{
				ChoiceID = Guid.NewGuid();
				RecommendedPermit = new List<Guid>();
			}
		}

		public class Suggestion
		{
			public string Message { get; set; }
			public bool RequireAllCondition { get; set; } = true;
			public SuggestionCondition[] Conditions { get; set; }
		}

		public class SuggestionCondition
		{
			public string DataKey { get; set; }
			public string[] ExpectedValues { get; set; }
            public string JSExpression { get; set; }

			public SuggestionCondition() { }

            public SuggestionCondition(string jsExpression)
            {
                this.JSExpression = jsExpression;
            }

			public SuggestionCondition(string dataKey, string[] expectedValues)
			{
				this.DataKey = dataKey;
				this.ExpectedValues = expectedValues;
			}

		}
	}

}
