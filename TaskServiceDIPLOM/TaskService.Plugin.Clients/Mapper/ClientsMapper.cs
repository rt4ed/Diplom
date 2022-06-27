using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Common;
using TaskService.Plugin.Clients.Models;

namespace TaskService.Plugin.Clients.Mapper
{
    public class ClientsMapper : CommonMapper
    {
        public ClientsDBModel Map(FileRow fileRow, int fieldsCount, ref List<TaskWarning> warnings)
        {
            if(fileRow is null)
            {
                warnings.Add(new TaskWarning { IsCritical = true, Message = "Row collection is empty!" });
                return null;
            }

            if (fileRow.RowValues is null)
            {
                warnings.Add(new TaskWarning { IsCritical = true, Message = "Row collection is empty!" });
                return null;
            }

            if (fileRow.RowValues.Count >= fieldsCount)
            {
                var model = new ClientsDBModel();
                var data = fileRow.RowValues.ToArray();
                var count = 0;

                model.AccountCode = ParseInt(data[count++], nameof(ClientsDBModel.AccountCode), ref warnings);

                model.CustomerAddress1 = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.CustomerAddress1), ref warnings);
                model.CustomerAddress2 = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.CustomerAddress2), ref warnings);
                model.CustomerAddress3 = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.CustomerAddress3), ref warnings);

                model.BirthDate = ParseDateTime(data[count++], nameof(ClientsDBModel.BirthDate), ref warnings);
                
                model.CustomerFirstName = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.CustomerFirstName), ref warnings);
                model.CustomerMiddleName = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.CustomerMiddleName), ref warnings);
                model.CustomerLastName = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.CustomerLastName), ref warnings);

                model.City = CutStringWithWarning(data[count++], 20, nameof(ClientsDBModel.City), ref warnings);
                model.State = CutStringWithWarning(data[count++], 20, nameof(ClientsDBModel.State), ref warnings);
                model.ZipCode = CutStringWithWarning(data[count++], 10, nameof(ClientsDBModel.ZipCode), ref warnings);
                model.EmailAddress = CutStringWithWarning(data[count++], 40, nameof(ClientsDBModel.EmailAddress), ref warnings);
                model.CellPhone = CutStringWithWarning(data[count++], 12, nameof(ClientsDBModel.CellPhone), ref warnings);
                model.Residency = CutStringWithWarning(data[count++], 5, nameof(ClientsDBModel.Residency), ref warnings);
                model.KPP = CutStringWithWarning(data[count++], 35, nameof(ClientsDBModel.KPP), ref warnings);
                model.OKATO = CutStringWithWarning(data[count++], 35, nameof(ClientsDBModel.OKATO), ref warnings);
                model.CountryCode = CutStringWithWarning(data[count++], 5, nameof(ClientsDBModel.CountryCode), ref warnings);

                model.Office_Name1 = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.Office_Name1), ref warnings);
                model.Office_Name2 = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.Office_Name2), ref warnings);
                model.Office_Name3 = CutStringWithWarning(data[count++], 30, nameof(ClientsDBModel.Office_Name3), ref warnings);

                model.IsClosed = ParseBool(data[count++], nameof(ClientsDBModel.IsClosed), ref warnings);
                model.CloseDate = ParseDateTime(data[count++], nameof(ClientsDBModel.CloseDate), ref warnings);

                return model;
            }
            else
            {
                warnings.Add(new TaskWarning { IsCritical = true, Message = "Row contain less fields than expected!" });
            }

            return null;
        }
    }
}
