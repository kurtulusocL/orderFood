using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CommentService
{
    public interface ICommentService
    {
        void Create(Comment model);
        void Update(Comment model);
        List<Comment> GetAllIncluding();
        Comment GetById(int? id);
        void Delete(Comment model);
        Comment HitRead(int? id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}
