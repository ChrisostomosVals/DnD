import ApiResponseModel from "../models/ApiResponseModel";
import CharacterArsenalModel from "../models/CharacterArsenalModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import InsertCharacterArsenalRequestModel from "../models/InsertArsenalRequestModel";
import UpdateCharacterArsenalRequestModel from "../models/UpdateCharacterArsenalRequestModel";
import { characterArsenalEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";



export default class CharacterArsenalApi{
    public static async GetAsync(token:string, url: string, characterId: string) : Promise<ApiResponseModel<Array<CharacterArsenalModel>>> {
        try {
            const uri = `${url}/${characterArsenalEndpoint}/${characterId}/all`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterArsenalModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterArsenalModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterArsenalModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterArsenalModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterArsenalModel[]>(null, ErrorResponseModel.NewError("CharacterArsenalApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<CharacterArsenalModel>> {
        try {
            const uri = `${url}/${characterArsenalEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterArsenalModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterArsenalModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterArsenalModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterArsenalModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterArsenalModel>(null, ErrorResponseModel.NewError("CharacterArsenalApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async InsertItemAsync(token:string, url: string, request: InsertCharacterArsenalRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterArsenalEndpoint}`;
            const response = await HttpClient.postAsync(token, uri, request)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterArsenalApi.InsertItemAsync().Exception", error));;
        }
    }
    public static async UpdateItemAsync(token:string, url: string, request: UpdateCharacterArsenalRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterArsenalEndpoint}`;
            const response = await HttpClient.putAsync(token, uri, request)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterArsenalApi.UpdateItemAsync().Exception", error));;
        }
    }
    public static async DeleteItemAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterArsenalEndpoint}/${id}/delete`;
            const response = await HttpClient.deleteAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterArsenalApi.DeleteItemAsync().Exception", error));;
        }
    }
}