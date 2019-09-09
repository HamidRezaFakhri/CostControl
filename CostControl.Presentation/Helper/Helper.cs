namespace CostControl.Presentation
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Globalization;
	using System.Linq;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Reflection;
	using CostControl.API.Models;
	using CostControl.BusinessEntity.Models.Base.Interfaces;
	using CostControl.BusinessEntity.Models.CostControl;

	public enum EnumTitle
	{
		Single = 0,
		List,
		Add,
		Edit,
		Delete,
		Import,
		Details
	}

	public class EntityTitle
	{
		public string TypeName { get; set; }

		public string SingleTitle { get; set; }

		public string PluralTitle { get; set; }
	}

	public static class Helper
	{
		private static List<EntityTitle> EntityTitles = new List<EntityTitle>
		{
			new EntityTitle{ TypeName = "CostPoint", SingleTitle = "مرکز هزینه", PluralTitle = "مراکز هزینه"},
			new EntityTitle{ TypeName = "ConsumptionUnit", SingleTitle = "واحد مصرفی", PluralTitle = "واحد های مصرفی"},
			new EntityTitle{ TypeName = "SalePoint", SingleTitle = "مرکز فروش", PluralTitle = "مراکز فروش"},
			new EntityTitle{ TypeName = "CostPointGroup", SingleTitle = "گروه مرکز هزینه", PluralTitle = "گروه مراکز هزینه"},
			new EntityTitle{ TypeName = "Setting", SingleTitle = "تنظیم", PluralTitle = "تنظیمات"},
			new EntityTitle{ TypeName = "Draft", SingleTitle = "حواله انبار", PluralTitle = "حواله های انبار"},
			new EntityTitle{ TypeName = "DraftItem", SingleTitle = "ردیف حواله انبار", PluralTitle = "ردیف های حواله انبار"},
			new EntityTitle{ TypeName = "Food", SingleTitle = "دستور غذایی", PluralTitle = "دستورهای غذایی"},
			new EntityTitle{ TypeName = "Recipe", SingleTitle = "قلم دستور غذایی", PluralTitle = "اقلام دستور غذایی"},
			new EntityTitle{ TypeName = "Ingredient", SingleTitle = "ماده اولیه", PluralTitle = "مواد خام"},
			new EntityTitle{ TypeName = "SaleCostPoint", SingleTitle = "مرکز هزینه مرکز فروش", PluralTitle = "مراکز هزینه مراکز فروش"},
			new EntityTitle{ TypeName = "OverCostType", SingleTitle = "سرفصل هزینه های سربار", PluralTitle = "سرفصل هزینه های سربار"},
			new EntityTitle{ TypeName = "OverCost", SingleTitle = "هزینه سربار", PluralTitle = "هزینه های سربار"},
			new EntityTitle{ TypeName = "IntakeRemittance", SingleTitle = "حواله مصرفی", PluralTitle = "حواله های مصرفی"},
			new EntityTitle{ TypeName = "Inventory", SingleTitle = "انبار", PluralTitle = "انبارها"},

			//new EntityTitle{ TypeName = "IntakeRemittanceItem", SingleTitle = "انبار", PluralTitle = "انبارها"},
			//new EntityTitle{ TypeName = "Menu", SingleTitle = "انبار", PluralTitle = "انبارها"},
			//new EntityTitle{ TypeName = "MenuItem", SingleTitle = "انبار", PluralTitle = "انبارها"},
			//new EntityTitle{ TypeName = "Buffet", SingleTitle = "انبار", PluralTitle = "انبارها"}
		};

		public static string GetEntityTitle<T>(EnumTitle enumTitle) where T : IBaseEntity
		{
			EntityTitle entity = EntityTitles.SingleOrDefault(e => e.TypeName.Equals(typeof(T).Name));

			if (entity == null)
			{
				return string.Empty;
			}

			switch (enumTitle)
			{
				case EnumTitle.Single:
					return entity.SingleTitle;
				case EnumTitle.List:
					return $"{entityListTitle}{entity.PluralTitle}";
				case EnumTitle.Add:
					return $"{addEntityTitle}{entity.SingleTitle} {newEntityTitle}";
				case EnumTitle.Edit:
					return $"{editEntityTitle}{entity.SingleTitle}";
				case EnumTitle.Delete:
					return $"{deleteEntityTitle}{entity.SingleTitle}";
				case EnumTitle.Import:
					return $"{insertEntityTitle}{entity.SingleTitle}";
				default:
					return entity.SingleTitle;
			}
		}

		private static readonly string entityListTitle = "فهرست ";

		private static readonly string addEntityTitle = "افزودن ";

		private static readonly string editEntityTitle = "ویرایش ";

		private static readonly string deleteEntityTitle = "حذف ";

		private static readonly string newEntityTitle = "جدید";

		private static readonly string insertEntityTitle = "درج ";

		private static readonly TimeSpan httpClientTimeout = new TimeSpan(0, 0, 30);

		public static string GetAPIAddress()
		{
#if DEBUG
			return "http://localhost:5001/api/";
			//return "http://79.175.155.6:5001/api/";
#elif !DEBUG
			return "http://79.175.155.6:5001/api/";
#endif
			//return "http://localhost/CostControl.Presentation/";
			//return "http://localhost/CostControl.API/api/";
		}

		public static string GetPresentationAddress()
		{
#if DEBUG
			return "http://localhost:5974/";
			//return "http://79.175.155.6:80/";
#elif !DEBUG
			return "http://79.175.155.6:80/";
#endif
			//return "http://localhost/CostControl.Presentation/";
		}

		public static string GetAuthenticationAddress()
		{
#if DEBUG
			return "http://127.0.0.1:89/api/um/";
			//return "http://79.175.155.6:89/api/um/";
#elif !DEBUG
			return "http://79.175.155.6:89/api/um/";
#endif
		}

		public static string ConverToPersian(this DateTime datetime)
		{
			var pDate = new PersianCalendar();
			var pMonth = pDate.GetMonth(datetime);
			var pMonthString = pMonth < 10 ? "0" + pMonth.ToString() : pMonth.ToString();
			var pDay = pDate.GetDayOfMonth(datetime);
			var pDayString = pDay < 10 ? "0" + pDay.ToString() : pDay.ToString();

			return $"{pDate.GetYear(datetime)}/{pMonthString}/{pDayString}";
		}

		public static string GetAPIAddress(string controllerName)
		{
			return string.IsNullOrEmpty(controllerName) ? GetAPIAddress() : GetAPIAddress() + $"{controllerName}/";
		}

		public static string GetAPIAddress(string controllerName, string actionName)
		{
			return string.IsNullOrEmpty(controllerName) ? GetAPIAddress() :
						(string.IsNullOrEmpty(actionName) ? GetAPIAddress(controllerName) : GetAPIAddress(controllerName) + $"{actionName}/");
		}

		public static ServiceResponse<T> GetServiceResponse<T>(string apiParams) where T : IBaseEntity
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.Timeout = httpClientTimeout;
					client.BaseAddress = new Uri(GetAPIAddress(typeof(T).Name));

					client.DefaultRequestHeaders.Clear();
					client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					var responseTask = client.GetAsync(apiParams);
					//EnsureSuccessStatusCode();
					responseTask.Wait();

					var result = responseTask.Result;

					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<ServiceResponse<T>>();

						readTask.Wait();

						return readTask.Result;
					}
					else
					{
						return null;
					}
				}
			}
			catch (OperationCanceledException ocex)
			{
				Console.WriteLine(ocex.Message);
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public static IEnumerable<T> GetServiceResponseList<T>(string apiParams) where T : IBaseEntity
		{
			using (var client = new HttpClient())
			{
				client.Timeout = httpClientTimeout;
				client.BaseAddress = new Uri(GetAPIAddress(typeof(T).Name));

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var responseTask = client.GetAsync(apiParams);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ServiceResponse<T>>();

					readTask.Wait();

					return readTask.Result.data;
				}
				else
				{
					return null;
				}
			}
		}

		public static IEnumerable<dynamic> GetServiceResponseList(string apiName, string apiParams)
		{
			using (var client = new HttpClient())
			{
				client.Timeout = httpClientTimeout;
				client.BaseAddress = new Uri(GetAPIAddress(apiName));

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var responseTask = client.GetAsync(apiParams);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IEnumerable<dynamic>>();

					readTask.Wait();

					return readTask.Result;
				}
				else
				{
					return null;
				}
			}
		}


		public static (bool result, string message) PostValueToSevice<T>(string actionName, T value) where T : IBaseEntity
		{
			//https://johnthiriet.com/efficient-post-calls/
			try
			{
				using (var client = new HttpClient())
				{
					client.Timeout = httpClientTimeout;
					client.BaseAddress = new Uri(GetAPIAddress(typeof(T).Name));

					var result = client.PostAsJsonAsync(actionName, value);
					result.Wait();
					HttpResponseMessage res = result.Result;

					if (res.IsSuccessStatusCode)
					{
						var p1 = res.Content.ReadAsStringAsync();
						string p = res.Content.ReadAsStringAsync().Result;

						return (true, "Ok");
					}
					else
					{
						return (false, "NOk");
					}
				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}

		/// <summary>
		/// Retrieves the <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" />
		/// of the current enum value, or the enum's member name if the <see cref="DisplayAttribute" /> is not present.
		/// </summary>
		/// <param name="val">This enum member to get the name for.</param>
		/// <returns>The <see cref="DisplayAttribute.Name" /> property on the <see cref="DisplayAttribute" /> attribute, if present.</returns>
		public static string GetDisplayName(this Enum val)
		{
			return val.GetType()
					  .GetMember(val.ToString())
					  .FirstOrDefault()
					  ?.GetCustomAttribute<DisplayAttribute>(false)
					  ?.Name
					  ?? val.ToString();
		}

		public static void Log(string message)
		{
			using (var writer = new System.IO.StreamWriter($@"C:\Temp\Log{Guid.NewGuid().ToString()}.txt"))
			{
				writer.WriteLine(DateTime.Now.ToString() + Environment.NewLine + message);
			}
		}
	}
}