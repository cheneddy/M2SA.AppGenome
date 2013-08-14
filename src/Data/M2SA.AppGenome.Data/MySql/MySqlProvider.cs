﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace M2SA.AppGenome.Data.MySql
{
    /// <summary>
    /// 
    /// </summary>
    public class MySqlProvider : IDatabaseProvider
    {
        static readonly char ParameterToken = '@';

        #region IDatabaseProvider 成员

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameterValues"></param>
        /// <param name="commandType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, IDictionary<string, object> parameterValues, CommandType commandType, int timeout)
        {
            return MySqlHelper.ExecuteNonQuery(this.ConnectionString, commandText, this.ConvertToDBParams(parameterValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameterValues"></param>
        /// <param name="commandType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string commandText, IDictionary<string, object> parameterValues, System.Data.CommandType commandType, int timeout)
        {
            return MySqlHelper.ExecuteDataset(this.ConnectionString, commandText, this.ConvertToDBParams(parameterValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameterValues"></param>
        /// <param name="commandType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, IDictionary<string, object> parameterValues, CommandType commandType, int timeout)
        {
            return MySqlHelper.ExecuteScalar(this.ConnectionString, commandText, this.ConvertToDBParams(parameterValues));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameterValues"></param>
        /// <param name="commandType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string commandText, IDictionary<string, object> parameterValues, CommandType commandType, int timeout)
        {
            return MySqlHelper.ExecuteReader(this.ConnectionString, commandText, this.ConvertToDBParams(parameterValues));
        }

        #endregion

        MySqlParameter[] ConvertToDBParams(IDictionary<string, object> parameterValues)
        {
            if (null == parameterValues || parameterValues.Count == 0)
            {
                return null;
            }

            var paramList = new MySqlParameter[parameterValues.Count];
            var paramIndex = 0;
            foreach(var item in parameterValues)
            {
                var paramName = this.BuildParameterName(item.Key);
                paramList[paramIndex] = new MySqlParameter(paramName, item.Value);
                paramIndex++;
            }

            return paramList;
        }

        string BuildParameterName(string name)
        {
            if (name[0] != ParameterToken)
            {
                return name.Insert(0, new string(ParameterToken, 1));
            }
            return name;
        }
    }
}