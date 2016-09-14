using Charon.Core.Entities;
using Charon.Core.Messaging;
using Charon.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Messaging
{
    public class DTOConverter
    {
        public object ConvertToDTO(object input)
        {
            var listConverter = new ListConverter();
            var updateConverter = new UpdateConverter();
            object[] listEntities = null;
            object[] listIncludeProps = null;
            object[] updateEntities = null;
            List<string> errors = null;

            //Get include properties and errors from interface
            var DTOWithProps = input as IDTOListIncludeProperties;
            if (DTOWithProps != null)
            {
                listIncludeProps = DTOWithProps.IncludeProperties;
                errors = (List<string>)DTOWithProps.Errors;
            }

            //Get list entities for conversion from interface
            var DTOList = input as IDTOList;
            if (DTOList != null)
            {
                listEntities = DTOList.Entities;
            }

            //Get update entities for conversion from interface
            var DTOUpdate = input as IDTOUpdate;
            if (DTOUpdate != null)
            {
                updateEntities = DTOUpdate.Entities;
            }

            //Convert list entities
            if (listEntities != null)
            {
                listConverter.ConvertToDTO(listEntities, listIncludeProps, errors);

                //Set properties back on request/response object via interfaces
                DTOList.Entities = listConverter.ConvertedItems.ToArray();

                //And add any errors back on too
                if (DTOWithProps != null)
                    DTOWithProps.Errors = listConverter.Errors;
            }

            //Convert update entities
            if (updateEntities != null)
            {
                updateConverter.ConvertToDTO(updateEntities);

                DTOUpdate.Entities = updateConverter.ConvertedItems.ToArray();
            }

            return input;
        }

        public object ConvertFromDTO(object input)
        {
            return input;
        }
    }
}
