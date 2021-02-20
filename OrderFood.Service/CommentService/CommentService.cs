using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void Create(Comment model)
        {
            _commentRepository.Create(model);
        }

        public void Delete(Comment model)
        {
            _commentRepository.Delete(model);
        }

        public List<Comment> GetAllIncluding()
        {
            return _commentRepository.GetAllIncluding();
        }

        public Comment GetById(int? id)
        {
            return _commentRepository.GetById(id);
        }

        public Comment HitRead(int? id)
        {
            return _commentRepository.HitRead(id);
        }

        public void SetActive(int id)
        {
            _commentRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _commentRepository.SetDeActive(id);
        }

        public void Update(Comment model)
        {
            _commentRepository.Update(model);
        }
    }
}
