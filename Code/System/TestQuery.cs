using System.Reflection;
using Colossal.Entities;
using Colossal.Logging;
using Colossal.Serialization.Entities;
using Game;
using Game.Agents;
using Game.Areas;
using Game.Buildings;
using Game.Citizens;
using Game.Common;
using Game.Companies;
using Game.Debug;
using Game.Economy;
using Game.Prefabs;
using Game.Serialization;
using Game.Simulation;
using Game.Vehicles;

//using HarmonyLib;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Resources = Game.Economy.Resources;

namespace IndustriesExtendedDLC.System
{
    public partial class TestQuery : GameSystemBase
    {
        private EntityQuery _testQuery;
        private EntityQuery _featureQuery;
        private EntityQuery _featureQuery2;
        private EntityQuery _featureQuery3;
        

        protected override void OnCreate()
        {
            base.OnCreate();
        }

        public void DoTesting()
        {
            _testQuery = SystemAPI.QueryBuilder().WithAll<EconomyParameterData>().Build();
            _featureQuery = SystemAPI.QueryBuilder().WithAll<Building>().WithAny<IndustrialProperty>().Build();
            _featureQuery2 = SystemAPI.QueryBuilder().WithAll<Building>().WithAny<PropertyRenter>().Build();
            _featureQuery3 = SystemAPI.QueryBuilder().WithAll<PropertyRenter>().Build();

            foreach (Entity entity in _testQuery.ToEntityArray(Allocator.Temp))
            {

                if (EntityManager.TryGetComponent(entity, out EconomyParameterData data))
                {
                    // All possible Economy params from the game that can be changed.
                    // I don't get the real propuse for each of them
                    /*                    
                    data.m_BuildRefundPercentage = (float3)data.m_BuildRefundPercentage + 0;
                    data.m_BuildRefundTimeRange = (float3)data.m_BuildRefundTimeRange + 0;
                    data.m_CityServiceWageAdjustment = (float)data.m_CityServiceWageAdjustment + 0;
                    data.m_CommercialEfficiency = (float)data.m_CommercialEfficiency + 0;
                    data.m_CommercialUpkeepLevelExponent = (float)data.m_CommercialUpkeepLevelExponent + 0;
                    data.m_CommuterWageMultiplier = (float)data.m_CommuterWageMultiplier + 0;
                    data.m_CompanyBankruptcyLimit = (int)data.m_CompanyBankruptcyLimit + 0;
                    data.m_FamilyAllowance = (int)data.m_FamilyAllowance + 0;
                    data.m_IndustrialEfficiency = (float)data.m_IndustrialEfficiency + 0;
                    data.m_IndustrialUpkeepLevelExponent = (float)data.m_IndustrialUpkeepLevelExponent + 0;
                    data.m_LandValueModifier = (float3)data.m_LandValueModifier + 0;
                    //data.m_MapTileUpkeepCostMultiplier               
                    data.m_MaxCitySpecializationBonus = (float)data.m_MaxCitySpecializationBonus + 0;
                    data.m_MixedBuildingCompanyRentPercentage = (float)data.m_MixedBuildingCompanyRentPercentage + 0;
                    data.m_Pension = (int)data.m_Pension + 0;
                    data.m_PerOfficeResourceNeededForIndustrial = (int)data.m_PerOfficeResourceNeededForIndustrial + 0;
                    data.m_PlayerStartMoney = (int)data.m_PlayerStartMoney + 0;
                    data.m_RentPriceBuildingZoneTypeBase = (float3)data.m_RentPriceBuildingZoneTypeBase + 0;
                    data.m_ResidentialMinimumEarnings = (int)data.m_ResidentialMinimumEarnings + 0;
                    data.m_ResidentialUpkeepLevelExponent = (float)data.m_ResidentialUpkeepLevelExponent + 0;
                    data.m_ResourceConsumptionMultiplier = (float2)data.m_ResourceConsumptionMultiplier + 0;
                    data.m_ResourceConsumptionPerCitizen = (float)data.m_ResourceConsumptionPerCitizen + 0;
                    data.m_ResourceProductionCoefficient = (int)data.m_ResourceProductionCoefficient + 0;
                    data.m_RoadRefundPercentage = (float3)data.m_RoadRefundPercentage + 0;
                    data.m_RoadRefundTimeRange = (float3)data.m_RoadRefundTimeRange + 0;
                    data.m_ShopPossibilityIncreaseDivider = (int)data.m_ShopPossibilityIncreaseDivider + 0;
                    data.m_TouristConsumptionMultiplier = (float)data.m_TouristConsumptionMultiplier + 0;
                    data.m_TrafficReduction = (float)data.m_TrafficReduction + 0;
                    data.m_TreeCostMultipliers = (int3)data.m_TreeCostMultipliers + 0;
                    data.m_UnemploymentAllowanceMaxDays = (float)data.m_UnemploymentAllowanceMaxDays + 0;
                    data.m_UnemploymentBenefit = (int)data.m_UnemploymentBenefit + 0;
                    data.m_Wage0 = (int)data.m_Wage0 + 0;
                    data.m_Wage1 = (int)data.m_Wage1 + 0;
                    data.m_Wage2 = (int)data.m_Wage2 + 0;
                    data.m_Wage3 = (int)data.m_Wage3 + 0;
                    data.m_Wage4 = (int)data.m_Wage4 + 0;
                    data.m_WorkDayEnd = (int)data.m_WorkDayEnd + 0;
                    data.m_WorkDayStart = (int)data.m_WorkDayStart + 0;
                    */

                    // This will impact of the export incoming for the natural resource?
                    data.m_ExtractorCompanyExportMultiplier = (float)data.m_ExtractorCompanyExportMultiplier + 0;
                    data.m_ExtractorCompanyExportMultiplier = Mod.Settings.ExtractorCompanyExportMultiplier;

                    // This value will increase the amount of natural resource harvested in a global way because
                    // it's apply to all the natural resources and for all the PreFrabs that acts as an extractor (Placeholders)
                    // I think the formula is ExtractorProductionEfficiency * BuildingEfficiency
                    data.m_ExtractorProductionEfficiency = (float)data.m_ExtractorProductionEfficiency + 0;
                    data.m_ExtractorProductionEfficiency = Mod.Settings.ExtractorProductionEfficiency;

                    // Apply the values to the game. I had to change it to be call in the main load.
                    // Now is executed from CRLT+E keybindings
                    EntityManager.SetComponentData(entity, data);
                }
            }

            //PrefabSystem prefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            RenterSystem renterSystem = World.GetOrCreateSystemManaged<RenterSystem>();

            

            foreach (Entity entity2 in _featureQuery.ToEntityArray(Allocator.Temp))
            {

                

                if (EntityManager.TryGetComponent(entity2, out PrefabRef data7))
                {
                    // data7.m_Prefab
                    int x = 0;
                }

                if (EntityManager.TryGetComponent(entity2, out IndustrialProperty data8))
                {
                    //data8.m_Resources = Game.Economy.Resource;
                    int x = 0;
                }

                if (EntityManager.TryGetComponent(entity2, out IndustrialProcessData data9))
                {
                    //data8.m_Resources = Game.Economy.Resource;
                    // data9.m_Input1 = ;
                    data9.m_IsImport = (byte)data9.m_IsImport;
                    data9.m_MaxWorkersPerCell = (float)data9.m_MaxWorkersPerCell + 0;
                    //data9.m_Output;
                    data9.m_WorkPerUnit = (int)data9.m_WorkPerUnit + 0;

                    
                }

                if (EntityManager.TryGetComponent(entity2, out Extractor data4))
                {
                    data4.m_ExtractedAmount = (float)data4.m_ExtractedAmount + 0;
                    data4.m_HarvestedAmount = (float)data4.m_HarvestedAmount + 0;
                    data4.m_MaxConcentration = (float)data4.m_MaxConcentration + 0; 
                    data4.m_ResourceAmount = (float)data4.m_ResourceAmount + 0;
                    data4.m_TotalExtracted = (float)data4.m_TotalExtracted + 0;
                    data4.m_WorkAmount = (float)data4.m_WorkAmount + 0;
                    //data4.m_WorkType = Game.Vehicles.VehicleWorkType.Collect;

                }

                if (EntityManager.TryGetComponent(entity2, out ExtractorParameterData data5))
                {
                    data5.m_FertilityConsumption = (float)data5.m_FertilityConsumption + 0;
                    data5.m_ForestConsumption = (float)data5.m_ForestConsumption + 0;
                    data5.m_FullFertility = (float)data5.m_FullFertility + 0;   
                    data5.m_FullOil = (float)data5.m_FullOil + 0;
                    data5.m_FullOre = (float)data5 .m_FullOre + 0;
                    data5.m_OilConsumption = (float)data5.m_OilConsumption + 0;
                    data5.m_OreConsumption = (float)data5.m_OreConsumption + 0;
                }

                if (EntityManager.TryGetComponent(entity2, out PropertyRenter data6))
                {
                    //data6.m_Property;
                    data6.m_Rent = (int)data6.m_Rent + 0;

                }

                if (EntityManager.TryGetBuffer(entity2, false, out DynamicBuffer<Renter> data11))
                {
                    int x = 0;

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out CompanyData data101))
                    {
                        int y = 0;
                        
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out IndustrialProperty data102))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out WorkProvider data103))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out Profitability data104))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out FreeWorkplaces data105))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out TaxPayer data106))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out ProcessingCompanyData data108))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out ExtractorCompanyData data109))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetComponent(data11[0].m_Renter, out ResourceData data110))
                    {
                        int y = 0;
                    }

                    if (EntityManager.TryGetBuffer(data11[0].m_Renter, false, out DynamicBuffer<Game.Economy.Resources> data111))
                    {
                        int y = 0;
                        //data111[0].m_Amount = (int)data111[0].m_Amount + 0;

                        Resources _resources = data111[0];
                        _resources.m_Amount = 0;
                        
                    }

                }



                if (EntityManager.TryGetComponent(entity2, out RentersUpdated data10))
                {
                    //data6.m_Property;
                    //data6.m_Rent = (int)data6.m_Rent + 0;
                    int x = 0;

                }


                if (EntityManager.TryGetComponent(entity2, out PlaceholderBuildingData data2))
                {
                    //data2.m_Type = BuildingType
                    //data2.m_ZonePrefab =
                    int x = 0;
                }


            }
        }

        protected override void OnUpdate()  
        {
        }

    }

}
