using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace PolyhydraGames.Extensions
{
//LEAVE ALONE!
#if NETSTANDARD2_0
    public static class ByteExtensions
    {
        /// <summary>
        /// Function to get object from byte array
        /// </summary>
        /// <param name="_ByteArray">byte array to get object</param>
        /// <returns>object</returns>
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
        public static T ToObject<T>(this byte[] byteArray) where T : class
        {
            try
            {
                // convert byte array to memory stream
                var memoryStream = new System.IO.MemoryStream(byteArray);

                // create new BinaryFormatter
                var binaryFormatter = new BinaryFormatter();

                // set memory stream position to starting point
                memoryStream.Position = 0;

                // Deserializes a stream into an object graph and return as a object.
                return binaryFormatter.Deserialize(memoryStream) as T;
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
#endif
}