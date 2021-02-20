using OrderFood.Service.CommentService;
using OrderFood.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Audit]
        public ActionResult _ProductComment(int? id)
        {
            return PartialView(_commentService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.ProductId == id).OrderByDescending(i => i.CreatedDate).Take(40).ToList());
        }

        [Audit]
        public ActionResult _CompanyComment(int? id)
        {
            return PartialView(_commentService.GetAllIncluding().Where(i => i.IsConfirmed == true && i.CompanyId == id).OrderByDescending(i => i.CreatedDate).Take(40).ToList());
        }
        public ActionResult _HitCount(int? id)
        {
            return PartialView(_commentService.HitRead(id));
        }

        [Audit]
        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult kurtulusocL(int page = 1)
        {
            return View(_commentService.GetAllIncluding().Where(i => i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 40));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Detail(int id)
        {
            return View(_commentService.GetById(id));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Product(int? id, int page = 1)
        {
            return View(_commentService.GetAllIncluding().Where(i => i.ProductId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult Company(int? id, int page = 1)
        {
            return View(_commentService.GetAllIncluding().Where(i => i.CompanyId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30));
        }

        [Authorize(Roles = "Admin,Helper,Asistant")]
        public ActionResult CommentList(int page = 1)
        {
            return View(_commentService.GetAllIncluding().OrderByDescending(i => i.CreatedDate).ToPagedList(page, 50));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Active(int id)
        {
            _commentService.SetActive(id);
            return RedirectToAction(nameof(CommentList));
        }

        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult DeActive(int id)
        {
            _commentService.SetDeActive(id);
            return RedirectToAction(nameof(kurtulusocL));
        }

        [Audit]
        [Authorize(Roles = "Admin,Asistant")]
        public ActionResult Delete(int id)
        {
            var deleteComment = _commentService.GetById(id);
            if (deleteComment != null)
            {
                _commentService.Delete(deleteComment);
            }
            return RedirectToAction(nameof(kurtulusocL));
        }
    }
}