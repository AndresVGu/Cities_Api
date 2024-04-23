using AndresVillarreal_project2.Data;
using AndresVillarreal_project2.Models;
using AndresVillarreal_project2.Utilities;
using AndresVillarreal_project2.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.ViewModels
{
    [QueryProperty(nameof(NeedCityRefresh), "refresh")]
    public partial class MainViewModel : BaseViewModel
    {
        //check
         readonly CityRepository _CityRepository;
        private readonly ProvinceRepository _ProvinceRepository;
        private bool needCityRefresh;

        [ObservableProperty]
        List<City> _Cities;

        [ObservableProperty]
        List<Province> _Provinces;

        public MainViewModel(CityRepository CityRepository, ProvinceRepository ProvinceRepository)
        {
            _CityRepository = CityRepository;
            _ProvinceRepository = ProvinceRepository;
            Cities = new List<City>();
            Provinces = new List<Province>();
            needCityRefresh = true;
            Title = "City List";
        }

        [ObservableProperty]
        private bool _NeedCityRefresh;

        [RelayCommand]
        async Task Appearing()
        {
          
            if (needCityRefresh)
            {
                await GetProvinces();
            }
            else if (NeedCityRefresh)
            {
                if(DDLProvinceSelectedIndex == 0)
                {
                    await ShowCities(null);
                }
                else
                {
                    DDLProvinceSelectedIndex = 0;
                }
            }
            SelectedCity = null;
        }

        [ObservableProperty]
        private int _DDLProvinceSelectedIndex = -1;

        [ObservableProperty]
        Province _SelectedProvince;

        partial void OnSelectedProvinceChanged(Province value)
        {
            _ = ShowCities(value?.ID);
        }

        [ObservableProperty]
        City _SelectedCity;

        partial void OnSelectedCityChanged(City value)
        {
            if (value == null)
                return;

            var data = new Dictionary<string, object>
            {
                {"Provinces", Provinces },
                {"City", value },
                {"Editing", true }
            };

            _ = Shell.Current.GoToAsync(nameof(CityDetailsPage), true, data);
        }

        [RelayCommand]
        private async Task Add()
        {
            City newCity = new()
            {
                ProvinceID =  1
            };

            var data = new Dictionary<string, object>
            {
                {"Provinces", Provinces },
                {"City", newCity },
                {"Editing", false }
            };

            await Shell.Current.GoToAsync(nameof(CityDetailsPage), true, data);
        }

       

        private async Task GetProvinces()
        {
            if(Provinces.Count == 0)
            {
                IsBusy = true;

                try
                {
                    Provinces = await _ProvinceRepository.GetProvinces();
                    
                    Provinces.Add(new Province { ID = 0, Name = "Select Province" });
                    Provinces = Provinces.OrderBy(x => x.Name).ToList();
                    needCityRefresh = false;
                    DDLProvinceSelectedIndex = 0;
                }
                catch (ApiException apiEx)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Errors:");
                    foreach (var error in apiEx.Errors)
                    {
                        sb.AppendLine("-" + error);
                    }
                    await Shell.Current.DisplayAlert("Problem Getting List of Provinces:", sb.ToString(), "Ok");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.GetBaseException().Message.Contains("connection with the server"))
                        {

                            await Shell.Current.DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                        }
                    }
                    else
                    {
                        if (ex.Message.Contains("NameResolutionFailure"))
                        {
                            await Shell.Current.DisplayAlert("Internet Access Error ", "Cannot resolve the Uri: " + Jeeves.DBUri.ToString(), "Ok");
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Error", ex.GetBaseException().Message, "Ok");
                        }
                    }
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        

        [RelayCommand]
        //int to string
        private async Task ShowCities(int? ProvinceID)
        {
            IsBusy = true;
            try
            {
                
                if(ProvinceID.GetValueOrDefault()>0)
                {
                    Cities = await _CityRepository.GetCitiesByProvince(ProvinceID.GetValueOrDefault());
                }
                else
                {
                    Cities = await _CityRepository.GetCities();
                }
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                await Shell.Current.DisplayAlert("Error Getting Cities:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {

                        await Shell.Current.DisplayAlert("Error", "No connection with the server. Check that the Web Service is running and available and then click the Refresh button.", "Ok");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "If the problem persists, please call your system administrator.", "Ok");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", ex.GetBaseException().Message, "Ok");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void RefreshData()
        {
            DDLProvinceSelectedIndex = 0;
        }



    }
}
