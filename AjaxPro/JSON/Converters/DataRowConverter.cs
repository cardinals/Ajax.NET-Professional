/*
 * MS	06-04-25	removed unnecessarily used cast
 * MS	06-05-23	using local variables instead of "new Type()" for get De-/SerializableTypes
 * MS	06-06-23	added AllowInheritance=true
 * 
 * 
 */
using System;
using System.Text;
using System.Data;

namespace AjaxPro
{
	/// <summary>
	/// Provides methods to serialize and deserialize a DataRow object.
	/// </summary>
	public class DataRowConverter : IJavaScriptConverter
	{
		public DataRowConverter() : base()
		{
			m_AllowInheritance = true;

			m_serializableTypes = new Type[] { typeof(DataRow) };
		}
	
		public override string Serialize(object o)
		{
			DataRow row = o as DataRow;

			if(row == null)
				throw new NotSupportedException();
			
			StringBuilder sb = new StringBuilder();

			DataColumnCollection cols = row.Table.Columns;
			int colcount = cols.Count;

			bool b = true;

			sb.Append("[");

			for(int i=0; i<colcount; i++)
			{
				if(b){ b = false; }
				else{ sb.Append(","); }

				sb.Append(JavaScriptSerializer.Serialize(row[cols[i].ColumnName]));
			}

			sb.Append("]");

			return sb.ToString();
		}
	}
}
