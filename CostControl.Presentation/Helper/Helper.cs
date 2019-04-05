namespace CostControl.Presentation
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
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
		Delete
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
			new EntityTitle{ TypeName = "Ingredient", SingleTitle = "ماده اولیه", PluralTitle = "انبار مواد خام"},
			new EntityTitle{ TypeName = "SaleCostPoint", SingleTitle = "مرکز هزینه مرکز فروش", PluralTitle = "مراکز هزینه مراکز فروش"},
			new EntityTitle{ TypeName = "OverCostType", SingleTitle = "سرفصل هزینه های سربار", PluralTitle = "سرفصل های هزینه های سربار"},
			new EntityTitle{ TypeName = "OverCost", SingleTitle = "هزینه سربار", PluralTitle = "هزینه های سربار"},
			new EntityTitle{ TypeName = "IntakeRemittance", SingleTitle = "حواله مصرفی", PluralTitle = "حواله های مصرفی"},
			new EntityTitle{ TypeName = "Inventory", SingleTitle = "انبار", PluralTitle = "انبارها"},

			//new EntityTitle{ TypeName = "IntakeRemittanceItem", SingleTitle = "انبار", PluralTitle = "انبارها"},
			//new EntityTitle{ TypeName = "Menu", SingleTitle = "انبار", PluralTitle = "انبارها"},
			//new EntityTitle{ TypeName = "MenuItem", SingleTitle = "انبار", PluralTitle = "انبارها"},
			//new EntityTitle{ TypeName = "Buffet", SingleTitle = "انبار", PluralTitle = "انبارها"}
		};

		public static string GetEntityTile<T>(EnumTitle enumTitle) where T : IBaseEntity
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
				default:
					return entity.SingleTitle;
			}
		}

		public static List<SaleCostPoint> GetSaleCostPoint()
		{
			ServiceResponse<SaleCostPoint> value = null;
			string str = string.Empty;
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(GetAPIAddress() + "SaleCostPoint/");

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				//HTTP GET
				System.Threading.Tasks.Task<HttpResponseMessage> responseTask = client.GetAsync("Get?PageNumber=1&PageSize=1000&searchKey=null&SortOrder=id&token=1");
				responseTask.Wait();

				HttpResponseMessage result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					System.Threading.Tasks.Task<ServiceResponse<SaleCostPoint>> readTask = result.Content.ReadAsAsync<ServiceResponse<SaleCostPoint>>();

					readTask.Wait();

					value = readTask.Result;

					return value.data as List<SaleCostPoint>;
				}
				else //web api sent error response 
				{
					//log response status here..

					value = null;

					//ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

					str = "Server error. Please contact administrator.";
				}

				return null;
			}
		}

		private static readonly string entityListTitle = "فهرست ";

		private static readonly string addEntityTitle = "افزودن ";

		private static readonly string editEntityTitle = "ویرایش ";

		private static readonly string deleteEntityTitle = "حذف ";

		private static readonly string newEntityTitle = "جدید";

		private static readonly TimeSpan httpClientTimeout = new TimeSpan(0, 0, 30);

		public static string GetAPIAddress()
		{
			return "http://localhost:5001/api/";
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
				using (HttpClient client = new HttpClient())
				{
					client.Timeout = httpClientTimeout;
					client.BaseAddress = new Uri(GetAPIAddress(typeof(T).Name));

					client.DefaultRequestHeaders.Clear();
					client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					System.Threading.Tasks.Task<HttpResponseMessage> responseTask = client.GetAsync(apiParams);
					//EnsureSuccessStatusCode();
					responseTask.Wait();

					HttpResponseMessage result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						System.Threading.Tasks.Task<ServiceResponse<T>> readTask = result.Content.ReadAsAsync<ServiceResponse<T>>();

						readTask.Wait();

						return readTask.Result;
					}
					else
					{
						return null;
					}
				}
			}
			catch (OperationCanceledException)
			{
				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static IEnumerable<T> GetServiceResponseList<T>(string apiParams) where T : IBaseEntity
		{
			using (HttpClient client = new HttpClient())
			{
				client.Timeout = httpClientTimeout;
				client.BaseAddress = new Uri(GetAPIAddress(typeof(T).Name));

				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				System.Threading.Tasks.Task<HttpResponseMessage> responseTask = client.GetAsync(apiParams);
				responseTask.Wait();

				HttpResponseMessage result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					System.Threading.Tasks.Task<ServiceResponse<T>> readTask = result.Content.ReadAsAsync<ServiceResponse<T>>();

					readTask.Wait();

					return readTask.Result.data;
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
				using (HttpClient client = new HttpClient())
				{
					client.Timeout = httpClientTimeout;
					client.BaseAddress = new Uri(GetAPIAddress(typeof(T).Name));

					System.Threading.Tasks.Task<HttpResponseMessage> result = client.PostAsJsonAsync(actionName, value);
					result.Wait();
					HttpResponseMessage res = result.Result;

					if (res.IsSuccessStatusCode)
					{
						System.Threading.Tasks.Task<string> p1 = res.Content.ReadAsStringAsync();
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
	}
}