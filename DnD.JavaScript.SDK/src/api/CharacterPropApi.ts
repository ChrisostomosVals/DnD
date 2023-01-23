import ApiResponseModel from "../models/ApiResponseModel";
import CharacterPropModel from "../models/CharacterPropModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import { InsertCharacterPropRequestModel } from "../models/InsertCharacterPropRequestModel";
import { UpdateCharacterPropRequestModel } from "../models/UpdateCharacterPropRequestModel";
import { characterPropEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";



export default class CharacterPropApi{
    public static async GetAsync(token:string, url: string, characterId: string) : Promise<ApiResponseModel<Array<CharacterPropModel>>> {
        try {
            const uri = `${url}/${characterPropEndpoint}/${characterId}/all`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterPropModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterPropModel[]>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterPropModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterPropModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterPropModel[]>(null, ErrorResponseModel.NewError("CharacterPropApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<CharacterPropModel>> {
        try {
            const uri = `${url}/${characterPropEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if(response.ok){
                const data = await response.json();
                if(data === null){
                    return new ApiResponseModel<CharacterPropModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<CharacterPropModel>(data, null);
            }
            else if(response.status == 400 || response.status == 404){
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<CharacterPropModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401){
                const error = response.statusText;
                return new ApiResponseModel<CharacterPropModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<CharacterPropModel>(null, ErrorResponseModel.NewError("CharacterPropApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async InsertAsync(token:string, url: string, request: InsertCharacterPropRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterPropEndpoint}`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterPropApi.InsertAsync().Exception", error));;
        }
    }
    public static async UpdateAsync(token:string, url: string, request: UpdateCharacterPropRequestModel) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterPropEndpoint}`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterPropApi.UpdateAsync().Exception", error));;
        }
    }
    public static async DeleteAsync(token:string, url: string, id: number) : Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${characterPropEndpoint}/${id}/delete`;
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
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("CharacterPropApi.DeleteAsync().Exception", error));;
        }
    }
}