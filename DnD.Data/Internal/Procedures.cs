using System;
using System.Collections.Generic;
using System.Text;

namespace DnD.Data.Internal
{
    public class Procedures
    {
        public class Characters
        {
            public const string Get = "dbo.Character_Get";
            public const string GetById = "dbo.Character_Get_ById";
            public const string GetBoss = "dbo.Character_Get_Boss";
            public const string GetHero = "dbo.Character_Get_Hero";
            public const string GetHostile = "dbo.Character_Get_Hostile";
            public const string GetnNpc = "dbo.Character_Get_Npc";
            public const string Create = "dbo.Character_Create";
            public const string Update = "dbo.Character_Update";
            public const string Delete = "dbo.Character_Delete";
            public class Gear
            {
                public const string Get = "dbo.Character_Get_Gear";
                public const string GetById = "dbo.Character_Get_Gear_ById";
                public const string CheckGear = "dbo.Character_Check_Gear";
                public const string GetMoney = "dbo.Character_Money_Get";
                public const string InsertItem = "dbo.Character_Insert_Gear";
                public const string UpdateItem = "dbo.Character_Update_Gear_Item";
                public const string TransferItem = "dbo.Character_Gear_Transfer";
                public const string UpdateQuantity = "dbo.Character_Update_Gear_Quantity";
                public const string DeleteItem = "dbo.Character_Delete_Geat_Item";
            }
            public class Skill
            {
                public const string Get = "dbo.Character_Get_Skills_ById";
                public const string GetById = "dbo.Character_Get_Skill_BySkillId";
                public const string GetBySkillIdAndCharacterId = "dbo.Character_Get_Skill_BySkillIdAndCharacterId";
                public const string Insert = "dbo.Character_Insert_Skill";
                public const string Update = "dbo.Character_Update_Skill";
            }
            public class MainStats
            {
                public const string Get = "dbo.Character_Get_MainStats";
                public const string GetById = "dbo.Character_Get_MainStats_ById";
                public const string Insert = "dbo.Character_Insert_MainStats";
                public const string Update = "dbo.Character_Update_MainStats";
            }
            public class Arsenal
            {
                public const string Get = "dbo.Character_Get_Arsenal";
                public const string GetById = "dbo.Character_Get_Arsenal_ById";
                public const string Insert = "dbo.Character_Arsenal_Insert";
                public const string Update = "dbo.Character_Update_Arsenal";
                public const string Delete = "dbo.Character_Arsenal_Delete";
            }
            public class Properties
            {
                public const string Get = "dbo.Character_Prop_Get";
                public const string GetById = "dbo.Character_Prop_Get_ById";
                public const string GetByType = "dbo.Character_Prop_Get_ByType";
                public const string Insert = "dbo.Character_Prop_Insert";
                public const string Update = "dbo.Character_Prop_Update";
                public const string Delete = "dbo.Character_Prop_Delete";
            }
        }
        public class Skill
        {
            public const string Get = "dbo.Skill_Get";
            public const string GetById = "dbo.Skill_Get_ById";
        }
        public class Class
        {
            public const string Get = "dbo.Class_Get";
            public const string GetById = "dbo.Class_Get_ById";
            public const string GetByCategoryId = "dbo.Class_Get_ByCategoryId";
        }
        public class ClassCategory
        {
            public const string Get = "dbo.ClassCategory_Get";
            public const string GetById = "dbo.ClassCategory_Get_ById";
        }
        public class WorldObject
        {
            public const string Get = "dbo.World_Object_Get";
            public const string GetById = "dbo.World_Object_Get_ById";
            public const string GetMap = "dbo.World_Object_Get_Map";
            public const string Create = "dbo.World_Object_Create";
            public const string Update = "dbo.World_Object_Update";
            public const string Delete = "dbo.World_Object_Delete";
            public class Prop
            {
                public const string GetByWorldId = "dbo.World_Object_Prop_Get_ByWorldId";
                public const string GetById = "dbo.World_Object_Prop_Get_ById";
                public const string GetMap = "dbo.World_Object_Get_Map_Prop";
                public const string Insert = "dbo.World_Object_Insert_Prop";
                public const string Update = "dbo.World_Object_Update_Prop";
                public const string Delete = "dbo.World_Object_Delete_Prop";
            }
        }
        
        public class WorldMisc
        {
            public const string Get = "dbo.World_Misc_Get";
            public const string GetById = "dbo.World_Misc_Get_ById";
            public const string GetByDependId = "dbo.World_Misc_Get_ByDependId";
            public const string Insert = "dbo.World_Misc_Insert";
            public const string Update = "dbo.World_Misc_Update";
        }
        public class Location
        {
            public const string Get = "dbo.Location_Get";
            public const string GetById = "dbo.Location_Get_ById";
            public const string GetLatest = "dbo.Location_Get_Latest";
            public const string Insert = "dbo.Location_Insert";
            public const string Update = "dbo.Location_Update";
            public const string Delete = "dbo.Location_Delete";
            public class Event
            {
                public const string Get = "dbo.Location_Event_Get";
                public const string GetById = "dbo.Location_Event_Get_ById";
                public const string Create = "dbo.Location_Event_Create";
                public const string Update = "dbo.Location_Event_Update";
                public const string Delete = "dbo.Location_Event_Delete";
            }
        }
        public class Race
        {
            public const string Get = "dbo.Race_Get";
            public const string GetById = "dbo.Race_Get_ById";
            public const string GetByCategoryId = "dbo.Race_Get_ByCategoryId";
        }
        public class RaceCategory
        {
            public const string Get = "dbo.Race_Category_Get";
            public const string GetById = "dbo.Race_Category_Get_ById";
        }
        public class User
        {
            public const string Get = "dbo.Users_Get";
            public const string GetById = "dbo.User_Get_ById";
            public const string Insert = "dbo.User_Insert";
            public const string Update = "dbo.User_Update";
            public const string UpdatePassword = "dbo.User_Update_Password";
            public const string UserValidate = "dbo.User_Validate";
            public const string CheckEmail = "dbo.User_Check_Email";
            public const string GetByEmail = "dbo.User_Get_ByEmail";
        }
        public class UserRole
        {
            public const string Get = "dbo.User_Role_Get";
        }
    }
}
