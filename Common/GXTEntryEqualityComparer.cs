using GTA_GXT_Editor.Utils;
using System;
using System.Collections.Generic;

namespace GTA_GXT_Editor.Common
{
    public class GXTEntryEqualityComparer : IEqualityComparer<GXTBase>
    {
        public bool Equals(GXTBase x, GXTBase y)
        {
            if (GetEqualityString(x).Equals(GetEqualityString(y)))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GXTBase obj)
        {
            return GetEqualityString(obj).GetHashCode();
        }

        private string GetEqualityString(GXTBase obj)
        {
            if (obj is GTAIII.GXTEntry)
            {
                return obj.DatName.GetClearName();
            }
            else if (obj is GTAVC.GXTEntry)
            {
                return obj.DatName.GetClearName() + (obj as GTAVC.GXTEntry).TableName.GetClearName();
            }
            else
            {
                throw new Exception("Неожиданная ошибка");
            }
        }
    }
}
