using AndresVillarreal_project2.Data;
using AndresVillarreal_project2.Models;
using AndresVillarreal_project2.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresVillarreal_project2.ViewModels
{
    [QueryProperty(nameof(Provinces), "Provinces")]
    [QueryProperty(nameof(City), "City")]
    [QueryProperty(nameof(Editing), "Editing")]
    public partial class CityDetailsViewModel : BaseViewModel
    {
        private readonly CityRepository _CityRepository;

        public CityDetailsViewModel(CityRepository CityRepository)
        {
            _CityRepository = CityRepository;
            Title = "Add New City";
        }

        [RelayCommand]
        void Appearing()
        {
            if (Editing)
            {
                Title = "City Details";
                SelectedProvince = City?.Province;
                SelectedProvinceIndex = Provinces.FindIndex(p => p.ID == SelectedProvince.ID);
            }
            else
            {
                SelectedProvince = Provinces[0];
                SelectedProvinceIndex = 0;
            }
        }

        [ObservableProperty]
        List<Province> _Provinces;
        [ObservableProperty]
        private City _City;
        [ObservableProperty]
        Province _SelectedProvince;
        [ObservableProperty]
        int _SelectedProvinceIndex;
        [ObservableProperty]
        bool _Editing;

        [RelayCommand]
        async Task Save()
        {
            try
            {
                City.Province = SelectedProvince;
                City.ProvinceID = (City.Province?.ID).GetValueOrDefault();

                if(City.ProvinceID > 0)
                {
                    if(City.ID == 0)
                    {
                        await _CityRepository.AddCity(City);
                    }
                    else
                    {
                        await _CityRepository.UpdateCity(City);
                    }
                    await Shell.Current.GoToAsync($"..?refresh=true");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Province Not Selected:", "You must select the Province for the City.", "Ok");
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
                await Shell.Current.DisplayAlert("Problem Saving the City:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    await Shell.Current.DisplayAlert("Error", "No connection with the server.", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", ex.GetBaseException().Message, "Ok");
                }
            }
        }


        [RelayCommand]
        async Task Delete()
        {
            var answer = await Shell.Current.DisplayAlert("Confirm Delete", "Are you certain that you want to delete " + City.Name + "?", "Yes", "No");
            if (answer == true)
            {
                try
                {
                    await _CityRepository.DeleteCity(City);
                    await Shell.Current.GoToAsync($"..?refresh=true");
                }
                catch (ApiException apiEx)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Errors:");
                    foreach (var error in apiEx.Errors)
                    {
                        sb.AppendLine("-" + error);
                    }
                    await Shell.Current.DisplayAlert("Problem Deleting the City:", sb.ToString(), "Ok");
                }
                catch (Exception ex)
                {
                    if (ex.GetBaseException().Message.Contains("connection with the server"))
                    {
                        await Shell.Current.DisplayAlert("Error", "No connection with the server.", "Ok");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", ex.GetBaseException().Message, "Ok");
                    }
                }
            }
        }

        [RelayCommand]
        async Task Cancel()
        {
            await Shell.Current.GoToAsync($"..?refresh=false");
        }

    }
}
