using BestPracticesDemo.Application.Responces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPracticesDemo.Application.Features.Categorys.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryDto Category { get; set; }
        public CreateCategoryCommandResponse(): base()
        {

        }
    }
}
