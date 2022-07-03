﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public FormService(IServiceProvider serviceProvider)
        {
            _formRepository = serviceProvider.GetRequiredService<IFormRepository>();
            _formAttributeRepository = serviceProvider.GetRequiredService<IFormAttributeRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
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
            var oldForm = _formRepository.Get(x => x.Id.Equals(model.Id)).FirstOrDefault();
            if (oldForm == null) throw new Exception("Form not found!");

            _mapper.Map(model, oldForm);
            _formRepository.Update(oldForm);

            _unitOfWork.SaveChange();

            return true;
        }

        public bool DeleteForm(string id)
        {
            var oldForm = _formRepository.Get(x => x.Id.Equals(id)).FirstOrDefault();
            if (oldForm == null) throw new Exception("Form not found!");

            _formRepository.Delete(oldForm);
            return true;
        }
    }
}