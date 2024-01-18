using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace PolyhydraGames.Extensions.Serialization
{


    public static class CsvReader
    {
        public static List<T> GetFromCsv<T>(string filename) where T : new()
        {

            // Check if the file exists
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found: " + filename);
                return null;
            }

            using (StreamReader reader = new StreamReader(filename))
            {
                // Read the CSV file into a DataTable
                var dataTable = ReadCsv(reader);
                return ConvertDataTable<T>(dataTable);
            };


        }

        public static List<T> GetFromCsv<T>(Stream stream) where T : new()
        {

            using (StreamReader reader = new StreamReader(stream))
            {
                // Read the CSV file into a DataTable
                var dataTable = ReadCsv(reader);
                return ConvertDataTable<T>(dataTable);
            };

        }

        static List<T> ConvertDataTable<T>(DataTable dataTable) where T : new()
        {
           var  resultList = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                var obj = new T();
                try
                {
                    foreach (DataColumn col in dataTable.Columns)
                    {

                        var property = typeof(T).GetProperty(col.ColumnName);

                        if (property == null || row[col] == DBNull.Value) continue;
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(obj, row[col].ToString());
                            continue;
                        }
                        else if (property.PropertyType == typeof(int))
                        {

                        }

                        object value = Convert.ChangeType(row[col], property.PropertyType);
                        property.SetValue(obj, value);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }



                resultList.Add(obj);
            }

            return resultList;
        }
        static DataTable ReadCsv(StreamReader reader)
        {
            var dataTable = new DataTable();

            try
            {
                // Read the first line to create columns
                var headers = reader.ReadLine().Split(',');
                foreach (var header in headers)
                {
                    dataTable.Columns.Add(header.Trim());
                }

                // Read the remaining lines as data
                while (!reader.EndOfStream)
                {
                    var data = reader.ReadLine().Split(',');
                    var row = dataTable.NewRow();

                    for (var i = 0; i < headers.Length; i++)
                    {
                        row[i] = data[i].Trim();
                    }

                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading CSV file: " + ex.Message);
            }

            return dataTable;
        }

        private static Tuple<T, IEnumerable<T>> HeadAndTail<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            var en = source.GetEnumerator();
            en.MoveNext();
            return Tuple.Create(en.Current, EnumerateTail(en));
        }

        private static IEnumerable<T> EnumerateTail<T>(IEnumerator<T> en)
        {
            while (en.MoveNext()) yield return en.Current;
        }

        public static IEnumerable<IList<string>> Parse(string content, char delimiter, char qualifier)
        {
            using (var reader = new StringReader(content))
                return Parse(reader, delimiter, qualifier);
        }

        public static Tuple<IList<string>, IEnumerable<IList<string>>> ParseHeadAndTail(TextReader reader, char delimiter, char qualifier)
        {
            return HeadAndTail(Parse(reader, delimiter, qualifier));
        }

        public static IEnumerable<IList<string>> Parse(TextReader reader, char delimiter, char qualifier)
        {
            var inQuote = false;
            var record = new List<string>();
            var sb = new StringBuilder();

            while (reader.Peek() != -1)
            {
                var readChar = (char)reader.Read();

                if (readChar == '\n' || (readChar == '\r' && (char)reader.Peek() == '\n'))
                {
                    // If it's a \r\n combo consume the \n part and throw it away.
                    if (readChar == '\r')
                        reader.Read();

                    if (inQuote)
                    {
                        if (readChar == '\r')
                            sb.Append('\r');
                        sb.Append('\n');
                    }
                    else
                    {
                        if (record.Count > 0 || sb.Length > 0)
                        {
                            record.Add(sb.ToString());
                            sb.Clear();
                        }

                        if (record.Count > 0)
                            yield return record;

                        record = new List<string>(record.Count);
                    }
                }
                else if (sb.Length == 0 && !inQuote)
                {
                    if (readChar == qualifier)
                        inQuote = true;
                    else if (readChar == delimiter)
                    {
                        record.Add(sb.ToString());
                        sb.Clear();
                    }
                    else if (char.IsWhiteSpace(readChar))
                    {
                        // Ignore leading whitespace
                    }
                    else
                        sb.Append(readChar);
                }
                else if (readChar == delimiter)
                {
                    if (inQuote)
                        sb.Append(delimiter);
                    else
                    {
                        record.Add(sb.ToString());
                        sb.Clear();
                    }
                }
                else if (readChar == qualifier)
                {
                    if (inQuote)
                    {
                        if ((char)reader.Peek() == qualifier)
                        {
                            reader.Read();
                            sb.Append(qualifier);
                        }
                        else
                            inQuote = false;
                    }
                    else
                        sb.Append(readChar);
                }
                else
                    sb.Append(readChar);
            }

            if (record.Count > 0 || sb.Length > 0)
                record.Add(sb.ToString());

            if (record.Count > 0)
                yield return record;
        }
    }
}