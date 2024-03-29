﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.ReportModule.Core;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;

namespace Thu_y.Modules.ReportModule.Adapters
{
    public class FormService: IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormAttributeRepository _formAttributeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportTicketRepository _reportTicketRepository;
        private readonly IReportTicketValueRepository _reportTicketValueRepository;

        public FormService(IServiceProvider serviceProvider)
        {
            _formRepository = serviceProvider.GetRequiredService<IFormRepository>();
            _formAttributeRepository = serviceProvider.GetRequiredService<IFormAttributeRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _reportTicketValueRepository = serviceProvider.GetRequiredService<IReportTicketValueRepository>();
            _reportTicketRepository = serviceProvider.GetRequiredService<IReportTicketRepository>();
        }

        public bool CreateForm(FormModel model)
        {
            var old = _formRepository.Get(x=>x.FormCode.ToLower().Equals(model.FormCode.ToLower())
            ||x.FormNumber.ToLower().Equals(model.FormNumber.ToLower()))
                .Any();
            if (old) throw new Exception("Form code/number existed!");

            var form = _mapper.Map(model, new FormEntity());
            _formRepository.Add(form);

            _unitOfWork.SaveChange();

            return true;
        }

        public bool UpdateForm(FormModel model)
        {
            //var oldForm = _formRepository.Get(x => x.Id.Equals(model.Id)).FirstOrDefault();
            //if (oldForm == null) throw new Exception("Form not found!");

            //_mapper.Map(model, oldForm);
            //_formRepository.Update(oldForm, oldForm => oldForm.FormName, oldForm => oldForm.FormNumber, oldForm => oldForm.FormCode, oldForm => oldForm.Status);

            //var oldAttrs = _formAttributeRepository.Get(x => x.FormId == model.Id);

            //var newAttrs = model.Attributes;
            //foreach(var attr in newAttrs)
            //{
            //    var old = oldAttrs.First(x => x.Name == attr.Name);
            //    if(old != null)
            //    {

            //    }
            //}
            //_formAttributeRepository.AddRange(oldAttrs.ToArray());


            //_unitOfWork.SaveChange();

            return true;
        }

        public bool DeleteForm(string code)
        {
            var oldForm = _formRepository.Get(x => x.Id.Equals(code) || x.FormCode.ToLower().Equals(code.ToLower())).FirstOrDefault();
            if (oldForm == null) throw new Exception("Form not found!");

            _formRepository.Delete(oldForm,true);
            _unitOfWork.SaveChange();
            return true;
        }

        public ICollection<string> RecommentAttribute(string attributeId)
        {
            return _reportTicketValueRepository.Get(_ => _.AttributeId == attributeId&& _.Value != null)
                                               .Select(x => x.Value)
                                               .Distinct()
                                               .ToList();
        }

        public FormModel GetSingleForm(string code, string refReportId)
        {
            code = code.ToLower();
            var entity = _formRepository.GetSingle(_ => _.FormCode == code || _.Id == code, false, _ => _.FormAttributes);
            var form = _mapper.Map<FormModel>(entity);
            if (string.IsNullOrEmpty(refReportId)) return form;
            var reportvalue = _reportTicketValueRepository.Get(_ => 
            _.ReportId == refReportId && _.AttributeCode != null)
                .Select(x => new { x.Value, x.AttributeCode });

            foreach (var value in reportvalue)
            {
                foreach (var item in form.Attributes)
                {
                    if (item.AttributeCode == value.AttributeCode)
                        item.Value = value.Value;
                }
            }

            return form;
        }

        public FormModel GetSingleForm(string code, string refReportId, string refReportNumber)
        {
            code = code.ToLower();
            var entity = _formRepository.GetSingle(_ => _.FormCode == code || _.Id == code, false, _ => _.FormAttributes);
            var mapfrom = _formRepository.GetSingle(_ => _.FormCode == "ĐK-KDĐV-001");
            var form = _mapper.Map<FormModel>(entity);
            if (string.IsNullOrEmpty(refReportId) && string.IsNullOrEmpty(refReportNumber)) return form;

            var reportvalues = _reportTicketRepository
                .Get(x => x.FormId == mapfrom.Id && (x.SerialNumber == refReportNumber || x.Id==refReportId))
                .Select(x=>x.Values)
                .FirstOrDefault()?
                .Where(x=>x.AttributeCode != null)
                .Select(x => new { x?.Value, x?.AttributeCode });

            if (reportvalues == null) return form;

            foreach (var value in reportvalues)
            {
                foreach (var item in form.Attributes)
                {
                    if (item.AttributeCode == value.AttributeCode)
                        item.Value = value.Value;
                }
            }

            return form;
        }
    }
}
