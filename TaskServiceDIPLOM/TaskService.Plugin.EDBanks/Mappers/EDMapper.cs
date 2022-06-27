using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TaskService.Plugin.EDBanks.Models;

namespace TaskService.Plugin.CBRTasks.Mappers
{
    public class EDMapper
    {
        public EDBanksModel Map(XmlNode node, DateTime businessDay)
        {
            if (node is null)
                throw new ArgumentNullException(nameof(EDMapper.Map));

            int resultInt = 0;
            decimal resultDec = 0;
            DateTime resultDt = DateTime.MinValue;

            var model = new EDBanksModel();

            model.BusinessDay = businessDay;
            model.BIC = node.Attributes?.GetNamedItem(nameof(EDBanksModel.BIC))?.Value;

            //Participant info
            var partInfo = node.FirstChild;
            var partInfoAttr = partInfo?.Attributes;

            if (partInfoAttr is not null)
            {
                model.NameP = partInfoAttr.GetNamedItem(nameof(EDBanksModel.NameP))?.Value;
                model.EnglName = partInfoAttr.GetNamedItem(nameof(EDBanksModel.EnglName))?.Value;

                if (int.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.RegN))?.Value, out resultInt))
                    model.RegN = resultInt;

                model.CntrCd = partInfoAttr.GetNamedItem(nameof(EDBanksModel.CntrCd))?.Value;

                if (int.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.Rgn))?.Value, out resultInt))
                    model.Rgn = resultInt;

                if (decimal.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.Ind))?.Value, out resultDec))
                    model.Ind = resultDec;

                model.Tnp = partInfoAttr.GetNamedItem(nameof(EDBanksModel.Tnp))?.Value;
                model.Nnp = partInfoAttr.GetNamedItem(nameof(EDBanksModel.Nnp))?.Value;
                model.Adr = partInfoAttr.GetNamedItem(nameof(EDBanksModel.Adr))?.Value;

                if (DateTime.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.DateIn))?.Value, out resultDt))
                    model.DateIn = resultDt;

                if (int.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.PtType))?.Value, out resultInt))
                    model.PtType = resultInt;

                if (int.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.Srvcs))?.Value, out resultInt))
                    model.Srvcs = resultInt;

                if (int.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.XchType))?.Value, out resultInt))
                    model.XchType = resultInt;

                if (int.TryParse(partInfoAttr.GetNamedItem(nameof(EDBanksModel.UID))?.Value, out resultInt))
                    model.UID = resultInt;

                model.ParticipantStatus = partInfoAttr.GetNamedItem(nameof(EDBanksModel.ParticipantStatus))?.Value;
            }

            var rstrNode = partInfo?.FirstChild;

            //  License check
            if (rstrNode is not null)
            {
                if (model.PtType.HasValue && model.PtType.Value == 90)
                    model.IsLicenseValid = false;
                else
                {
                    var rstr = rstrNode.Attributes?.GetNamedItem("Rstr")?.Value;
                    var rstrDate = Convert.ToDateTime(rstrNode.Attributes?.GetNamedItem("RstrDate")?.Value);

                    if (rstr == "LWRS" && rstrDate < DateTime.Now)
                        model.IsLicenseValid = false;
                }
            }

            if (!model.IsLicenseValid.HasValue)
                model.IsLicenseValid = true;

            //Accounts
            var accs = node.LastChild;

            if (accs is not null)
            {
                var accAttr = accs.Attributes;

                if (accAttr is not null)
                {
                    if (decimal.TryParse(accAttr.GetNamedItem(nameof(EDBanksModel.Account))?.Value, out resultDec))
                        model.Account = resultDec;

                    model.RegulationAccType = accAttr.GetNamedItem(nameof(EDBanksModel.RegulationAccType))?.Value;
                    model.AccountCBRBIC = accAttr.GetNamedItem(nameof(EDBanksModel.AccountCBRBIC))?.Value;

                    if (DateTime.TryParse(accAttr.GetNamedItem(nameof(EDBanksModel.DateIn))?.Value, out resultDt))
                        model.DateIn = resultDt;

                    model.AccountStatus = accAttr.GetNamedItem(nameof(EDBanksModel.AccountStatus))?.Value;
                }
            }

            return model;
        }
    }
}
