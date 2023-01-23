using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace PolyhydraGames.Extensions
{
    public static class BinarySerialization
    {

        /// <summary>
        /// Function to get object from byte array
        /// </summary>
        /// <param name="_ByteArray">byte array to get object</param>
        /// <returns>object</returns>
        [Obsolete]
        public static byte[] ToByteArray<T>(this T item) where T : class
        {
            try
            {
                // convert byte array to memory stream
                var memoryStream = new System.IO.MemoryStream()
                {
                    Position = 0
                };

                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, item);
                return memoryStream.GetBuffer();
            }
            catch (Exception exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", exception.ToString());
            }

            // Error occured, return null
            return null;
        }

        /// <summary>
        /// Function to get object from byte array
        /// </summary>
        /// <param name="_ByteArray">byte array to get object</param>
        /// <returns>object</returns>
        [Obsolete]
        public static T ToObject<T>(this byte[] _ByteArray) where T : class
        {
            try
            {
                // convert byte array to memory stream
                var _MemoryStream = new System.IO.MemoryStream(_ByteArray);

                // create new BinaryFormatter
                var _BinaryFormatter
                    = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // set memory stream position to starting point
                _MemoryStream.Position = 0;

                // Deserializes a stream into an object graph and return as a object.
                return _BinaryFormatter.Deserialize(_MemoryStream) as T;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return null;
        }
    }
}