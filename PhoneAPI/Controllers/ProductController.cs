﻿using PhoneAPI.Models.DAO;
using PhoneAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PhoneAPI.Controllers
{
    public class ProductController : ApiController
    {
        [Route("Api/ProductController/GetAllProduct")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProduct()
        {
            return Ok(await ProductDAO.Instance.GetAllProduct());
        }


        [Route("Api/ProductController/UploadProductImage")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage UploadProductImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/Assets/Images/Product/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [Route("Api/ProductController/AddProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddProduct(ProductDTO productDTO)
        {
            return Ok(await ProductDAO.Instance.AddProduct(productDTO));
        }

        [Route("Api/ProductController/UpdateProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateBrand(ProductDTO product)
        {
            return Ok(await ProductDAO.Instance.UpdateProduct(product));
        }

        [Route("Api/ProductController/DeleteProduct/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> DeleteProduct(int ID)
        {
            return Ok(await ProductDAO.Instance.DeleteProduct(ID));
        }
/*
        [Route("Api/ProductController/RestoreAllProduct")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> RestoreAllProduct()
        {
            return Ok(await ProductDAO.Instance.RestoreAllProduct());
        }*/

        [Route("Api/ProductController/AddProductDetail")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddProductDetail(ProductDetailDTO productDetail)
        {
            return Ok(await ProductDetailDAO.Instance.AddProductDetail(productDetail));
        }

        [Route("Api/ProductController/GetProductDetailByProductId/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductDetailByProductId(int ID)
        {
            return Ok(await ProductDetailDAO.Instance.GetProductDetailByProductId(ID));
        }

        /*[Route("Api/ProductController/IncreaseViewCount/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> IncreaseViewCount(int ID)
        {
            return Ok(await ProductDAO.Instance.IncreaseViewCount(ID));
        }*/

        [Route("Api/ProductController/GetProductByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductByID(int ID)
        {
            return Ok(await ProductDAO.Instance.GetProductByID(ID));
        }
        
        [Route("Api/ProductController/GetTopSaleProduct")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetTopSaleProduct()
        {
            return Ok(await ProductDAO.Instance.GetTopSaleProduct());
        }

        [Route("Api/ProductController/AddProductVersion")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddProductVersion(ProductVersionDTO productVersionDTO)
        {
            return Ok(await ProductVersionDAO.Instance.AddProductVersion(productVersionDTO));
        }

        [Route("Api/ProductController/GetProductVersionByProductId/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductVersionByProductId(int ID)
        {
            return Ok(await ProductVersionDAO.Instance.GetProductVersionByProductId(ID));
        }
    }
}
