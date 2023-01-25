

import ApiResponseModel from "../models/ApiResponseModel";
import LocationModel from "../models/LocationModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import { locationEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";
import InsertLocationRequestModel from "../models/InsertLocationRequestModel";
import UpdateLocationRequestModel from "../models/UpdateLocationRequestModel";



export default class LocationApi {
    public static async GetAsync(token: string, url: string, latest: boolean): Promise<ApiResponseModel<LocationModel[]> | ApiResponseModel<LocationModel>> {
        try {
            if (latest) {
                const uri = `${url}/${locationEndpoint}/?latest=true`;
                const response = await HttpClient.getAsync(token, uri)
                if (response.ok) {
                    const data = await response.json();
                    if (data === null) {
                        return new ApiResponseModel<LocationModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel<LocationModel>(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = await response.json();
                    const error = response.statusText;
                    return new ApiResponseModel<LocationModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel<LocationModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
                }
            }
            else {
                const uri = `${url}/${locationEndpoint}`;
                const response = await HttpClient.getAsync(token, uri)
                if (response.ok) {
                    const data = await response.json();
                    if (data === null) {
                        return new ApiResponseModel<LocationModel[]>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                    }
                    return new ApiResponseModel<LocationModel[]>(data, null);
                }
                else if (response.status == 400 || response.status == 404) {
                    const errorMsg = await response.json();
                    const error = response.statusText;
                    return new ApiResponseModel<LocationModel[]>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
                }
                else if (response.status == 401) {
                    const error = response.statusText;
                    return new ApiResponseModel<LocationModel[]>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
                }
            }

        } catch (error) {
            return new ApiResponseModel<LocationModel[]>(null, ErrorResponseModel.NewError("LocationApi.GetAsync().Exception", error));;
        }
    }
    public static async GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<LocationModel>> {
        try {
            const uri = `${url}/${locationEndpoint}/${id}`;
            const response = await HttpClient.getAsync(token, uri)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<LocationModel>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<LocationModel>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<LocationModel>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<LocationModel>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<LocationModel>(null, ErrorResponseModel.NewError("LocationApi.GetByIdAsync().Exception", error));;
        }
    }
    public static async CreateAsync(token: string, url: string, request: InsertLocationRequestModel): Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${locationEndpoint}`;
            const response = await HttpClient.postAsync(token, uri, request)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("LocationApi.CreateAsync().Exception", error));;
        }
    }
    public static async UpdateAsync(token: string, url: string, request: UpdateLocationRequestModel): Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${locationEndpoint}`;
            const response = await HttpClient.putAsync(token, uri, request)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("LocationApi.UpdateAsync().Exception", error));;
        }
    }
    public static async DeleteAsync(token: string, url: string, id: string): Promise<ApiResponseModel<void>> {
        try {
            const uri = `${url}/${locationEndpoint}/${id}/delete`;
            const response = await HttpClient.deleteAsync(token, uri)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<void>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<void>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<void>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }
        } catch (error) {
            return new ApiResponseModel<void>(null, ErrorResponseModel.NewError("LocationApi.DeleteAsync().Exception", error));;
        }
    }
}