using ApplicatonProcess.Web.Models;
using ApplicatonProcess.Web.Services;
using FluentValidation.Results;
using ApplicatonProcess.Domain.Repsitories;
using ApplicatonProcess.Domain.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicatonProcess.Domain.Models;
using System;
using Swashbuckle.AspNetCore.Annotations;
using static ApplicatonProcess.Web.Models.BaseReponseModel<object>;

namespace ApplicatonProcess.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IHttpClient _httpClient;
        private readonly IAssetDetailService _assetDetailService;

        public AssetsController(IAssetDetailService assetDetailService, IHttpClient httpClient)
        {
            _httpClient = httpClient;
            _assetDetailService = assetDetailService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get an asset detail items")]
        [SwaggerResponse(200, "The asset detail objects were found", typeof(BaseReponseModel<IEnumerable<AssetSuccessResponseModel>>))]
        public async Task<ActionResult<IEnumerable<AssetSuccessResponseModel>>> GetAssets()
        {
            var result = await _assetDetailService.GetAssetDetails();
            var successResponseModel = new BaseReponseModel<IEnumerable<AssetSuccessResponseModel>>();
            successResponseModel.Success = true;
            successResponseModel.Data = result;
            return Ok(successResponseModel);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an asset detail item by id")]
        [SwaggerResponse(200, "The asset detail object was found", typeof(BaseReponseModel<ResponseMessage>))]
        [SwaggerResponse(404, "An asset with the provided id wasn't found", typeof(BaseReponseModel<object>))]
        public async Task<ActionResult<AssetRequestModel>> GetAssets(int id)
        {
            var successModel = await _assetDetailService.GetAssetDetailsById(id);
            return Ok(successModel);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new asset detail item")]
        [SwaggerResponse(201, "The asset detail object was created", typeof(BaseReponseModel<ResponseMessage>))]
        [SwaggerResponse(400, "The asset detail data is invalid", typeof(BaseReponseModel<object>))]
        [SwaggerResponse(500, "There was an issue saving asset detail data", typeof(BaseReponseModel<object>))]
        public async Task<ActionResult<AssetSuccessResponseModel>> PostAssets([FromBody] AssetRequestModel assetRequestModel)
        {
            AssetValidator assetValidator = new AssetValidator(_httpClient);
            var asset = ModelConvert.ConvAssetRequestToModel(assetRequestModel);
            
            ValidationResult validationResult = assetValidator.Validate(asset);
            var assetErrorResponseModel = new BaseReponseModel<object>();
            var successResponseModel = new BaseReponseModel<object>();
            if (!validationResult.IsValid)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = validationResult.Errors.Select(x => x.ToString()).ToArray();
                return BadRequest(assetErrorResponseModel);
            }

            var successModel = await _assetDetailService.SaveAssetDetails(asset);
            if (successModel > 0)
            {
                dynamic responseMessage = new { message = "Asset details saved successfully" };
                successResponseModel.Success = true;
                successResponseModel.Data = responseMessage;
            } else
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Asset details could not be save because of an error from our end, please try again" };
                return StatusCode(StatusCodes.Status500InternalServerError, assetErrorResponseModel);
            }

            return StatusCode(StatusCodes.Status201Created, successResponseModel);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an asset detail item by id")]
        [SwaggerResponse(200, "The asset detail object was updated", typeof(BaseReponseModel<ResponseMessage>))]
        [SwaggerResponse(400, "The asset detail did wasn't provided", typeof(BaseReponseModel<object>))]
        [SwaggerResponse(404, "An asset with the provided id wasn't found", typeof(BaseReponseModel<object>))]
        [SwaggerResponse(500, "There was an issue saving asset detail data", typeof(BaseReponseModel<object>))]
        public async Task<ActionResult> PutAssets([FromRoute]int id, [FromBody] AssetRequestModel assetRequestModel)
        {
            var asset = ModelConvert.ConvAssetRequestToModel(assetRequestModel);
            var assetErrorResponseModel = new BaseReponseModel<object>();
            var successResponseModel = new BaseReponseModel<object>();
            if (id == null)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Please provide the Id" };
                return BadRequest(assetErrorResponseModel);
            }

            var result = await _assetDetailService.UpdateAssetDetails(id, asset);
            if (result == null)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "A record with the id you specified does not exist" };
                return NotFound(assetErrorResponseModel);
            }

            if (result.Item1 > 0)
            {
                dynamic responseMessage = new { message = "Asset details saved successfully" };
                successResponseModel.Data = responseMessage;
            }
            else
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Asset details could not be updated because of an error from our end, please try again" };
                return StatusCode(StatusCodes.Status500InternalServerError, assetErrorResponseModel);
            }
            return Ok(successResponseModel);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an asset detail item by id")]
        [SwaggerResponse(200, "The asset detail object was deleted", typeof(BaseReponseModel<ResponseMessage>))]
        [SwaggerResponse(404, "An asset with the provided id wasn't found", typeof(BaseReponseModel<object>))]
        [SwaggerResponse(500, "There was an issue deleting asset detail data", typeof(BaseReponseModel<object>))]
        public async Task<ActionResult> DeleteAssets(int id)
        {
            var result = await _assetDetailService.DeleteAssetDetailsById(id);
            var assetErrorResponseModel = new BaseReponseModel<object>();
            var successResponseModel = new BaseReponseModel<object>();
            if (result == null)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "A record with the id you specified does not exist" };
                return NotFound(assetErrorResponseModel);
            }

            if (result.Item1 > 0)
            {
                dynamic responseMessage = new { message = "Asset details deleted successfully" };
                successResponseModel.Data = responseMessage;
            }
            else
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Asset details could not be deleted because of an error from our end, please try again" };
                return StatusCode(StatusCodes.Status500InternalServerError, assetErrorResponseModel);
            }
            return Ok(successResponseModel);
        }
    }
}
