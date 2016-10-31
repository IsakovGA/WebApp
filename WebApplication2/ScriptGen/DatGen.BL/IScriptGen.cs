﻿using DatGen.Dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatGen.BL
{
    public interface IScriptGen
    {
        UserEntity GenerateUser();
        string GetValueLine(UserEntity entity);
        string GetInsertLine(string tableName);
        string CreateScript(int entityCount, string tableName);

    }
}
