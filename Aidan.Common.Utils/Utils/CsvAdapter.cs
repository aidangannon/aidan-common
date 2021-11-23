using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;
using CsvHelper;

namespace Aidan.Common.Utils.Utils
{
    public class CsvAdapter : ICsvAdapter
    {
        private readonly string _filePath;

        public CsvAdapter( string filePath ) { _filePath = filePath; }
        
        public ObjectResult<IEnumerable<T>> Read<T>( )
        {
            try
            {
                using( var reader = new StreamReader( _filePath ) )
                using( var csv = new CsvReader( reader, CultureInfo.InvariantCulture ) )
                {
                    var records = csv.GetRecords<T>( );
                    return new ObjectResult<IEnumerable<T>>
                    {
                        Status = OperationResultEnum.Success,
                        Value = records
                    };
                }
            }
            catch( Exception e )
            {
                return new ObjectResult<IEnumerable<T>>
                {
                    Status = OperationResultEnum.Failed,
                    Msg = e.Message
                };
            }
        }

        public Result Write<T>( IEnumerable<T> data )
        {
            try
            {
                using( var writer = new StreamWriter( _filePath ) )
                using( var csv = new CsvWriter( writer, CultureInfo.InvariantCulture ) ) { csv.WriteRecords( data ); }

                return Result.Success( );
            }
            catch( Exception e )
            {
                return Result.Error( e.Message );
            }
        }
    }
}