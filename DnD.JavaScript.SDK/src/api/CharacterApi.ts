import ApiResponseModel from "../models/ApiResponseModel";
import CharacterModel from "../models/CharacterModel";
import CreateCharacterRequestModel from "../models/CreateCharacterRequestModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import UpdateCharacterRequestModel from "../models/UpdateCharacterRequestModel";
import { characterEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";


export default class CharacterApi{
    public static async GetAsync(token:string, url: string) : Promise<ApiResponseModel<Array<CharacterModel>>> {
        try {
            const uri = `${url}/${characterEndpoint}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterModel[]>(null, ErrorResponseModel.NewError("CharacterApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token:string, url: string, id: string) : Promise<ApiResponseModel<CharacterModel>> {
        try {
            const uri = `${url}/${characterEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterModel>(null, ErrorResponseModel.NewError("CharacterApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async CreateAsync(token:string, url: string, request: CreateCharacterRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterEndpoint}`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterApi.CreateAsync().Exception", error));;
        }
    }
    public static async UpdateAsync(token:string, url: string, request: UpdateCharacterRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterEndpoint}`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterApi.UpdateAsync().Exception", error));;
        }
    }
    public static async DeleteAsync(token:string, url: string, id: string) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterEndpoint}/${id}/delete`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterApi.DeleteAsync().Exception", error));;
        }
    }
}