using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class Query
    {
        #region Declaration
        private List<string> _Fields;
        private List<string> _Values;
        private List<bool> _Key;
        private string _Table;
        #endregion //Declaration

        #region Constructor(s)
        internal Query()
        {
            //Initialize default values
            InitializeDefaults();
        }
        #endregion //Constructor(s)

        #region Property(s)
        internal string Table
        {
            get { return _Table; }
            set { _Table = value; }
        }
        #endregion //Property(s)

        #region Internal Method(s)
        
        internal void AddToQuery(string field, string value)
        {
            AddToQuery(field, value, false);
        }
        internal void AddToQuery(string field, string value, bool key)
        {
            _Fields.Add(field);
            if (value != null)
                value = "'" + value.Replace("'", "''") + "'";
            else if (value == null)
                value = "null";
            _Values.Add(value);
            _Key.Add(key);
        }
        internal void AddToQuery(string field, double value)
        {
            AddToQuery(field, value, false);
        }
        internal void AddToQuery(string field, double value, bool key)
        {
            _Fields.Add(field);
            _Values.Add(value.ToString());
            _Key.Add(key);
        }

        internal void AddToQuery(string field, int value)
        {
            AddToQuery(field, value, false);
        }
        internal void AddToQuery(string field, int value, bool key)
        {
            _Fields.Add(field);
            _Values.Add(value.ToString());
            _Key.Add(key);
        }

        internal string InsertQuery()
        {
            string sql = "", fields = "", values = "";
            try
            {
                fields = string.Join(",", _Fields.ToArray());
                fields = "(" + fields + ")";
                values = string.Join(",", _Values.ToArray());
                values = "(" + values + ")";
                sql = "Insert into " + _Table;
                sql = sql + " " + fields + " Values" + values;
                sql = sql + ";  select scope_identity();"; 
            }
            catch (Exception ex)
            {
                sql = "";
                Log.WriteError(ex.ToString());
            }

            return sql;
        }

        internal string UpdateQuery()
        {
            string sql = "", set = "", where = "";
            int index;
            try
            {
                for (index = 0; index < _Fields.Count; index++)
                {
                    if (!_Key[index])
                        set = set + _Fields[index] + "=" + _Values[index] + ",";
                    else
                        where = where + _Fields[index] + "=" + _Values[index] + " And ";
                }

                if (where.Trim().Length != 0)
                    where = where.Substring(0, where.Length - " And ".Length);

                if (set.Trim().Length != 0)
                    set = set.Substring(0, set.Length - ",".Length);

                sql = "Update " + _Table + " Set " + set + " Where " + where;
            }
            catch (Exception ex)
            {
                sql = "";
                Log.WriteError(ex.ToString());
            }

            return sql;
        }

        internal string UpdateQuery(string whereCluase)
        {
            string sql = "", set = "", where = "";
            int index;
            try
            {
                for (index = 0; index < _Fields.Count; index++)
                    set = set + _Fields[index] + "=" + _Values[index] + ",";

                set = set.Substring(0, set.Length - ",".Length);
                where = whereCluase;
                sql = "Update " + _Table + " Set " + set + " Where " + where;
            }
            catch (Exception ex)
            {
                sql = "";
                Log.WriteError(ex.ToString());
            }

            return sql;
        }

        internal string DeleteQuery()
        {
            string sql = "", where = "";
            int index;
            try
            {
                for (index = 0; index < _Fields.Count; index++)
                {
                    where = where + _Fields[index] + "=" + _Values[index] + " And ";
                }

                if (where.Trim().Length != 0)
                    where = where.Substring(0, where.Length - " And ".Length);

                sql = "Delete From " + _Table + " Where " + where;
            }
            catch (Exception ex)
            {
                sql = "";
                Log.WriteError(ex.ToString());
            }

            return sql;
        }
   
        internal string UniqueNameQuery()
        {
            string sql = "", fields = "", where = "";
            int index;
            try
            {
                fields = string.Join(",", _Fields.ToArray());              

                for (index = 0; index < _Fields.Count; index++)
                {
                    where = where + _Fields[index] + "=" + _Values[index] + " And ";
                }

                if (where.Trim().Length != 0)
                    where = where.Substring(0, where.Length - " And ".Length);

                //sql = "Select From " + _Table + " Where " + where;

                //sql = "Insert into " + _Table;
                sql = "Select " + fields + " From " + _Table + " Where " + where;
            }
            catch (Exception ex)
            {
                sql = "";
                Log.WriteError(ex.ToString());
            }

            return sql;
        }
        #endregion //Internal Method(s)

        #region Private Methods
        private void InitializeDefaults()
        {
            _Fields = new List<string>();
            _Values = new List<string>();
            _Key = new List<bool>();
            _Table = "";
        }
        #endregion
    }
}
