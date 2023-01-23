import ApiResponseModel from "../models/ApiResponseModel";
import ErrorResponseModel from "../models/ErrorResponseModel";
import { mediaEndpoint } from "../utils/constants";
import HttpClient from "../utils/httpService";


export class MediaApi {
    public static async UploadAsync(token: string, url: string, type: string, name: string, files: File[]): Promise<ApiResponseModel<string>> {
        try {
            const uri = `${url}/${mediaEndpoint}/upload`
            const formData = new FormData();
            formData.append("type", type)
            formData.append("name", name)
            for (const file of files) {
                formData.append('file', file, file.name)
            }

            const response = await fetch(uri, {
                method: "POST",
                body: formData,
                headers: {
                    "Authorization": `Bearer ${token}`,
                }
            });
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<string>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<string>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<string>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
        } catch (error) {
            return new ApiResponseModel<string>(null, ErrorResponseModel.NewError("MediaApi.UploadAsync().Exception", error));;
        }
    }
    public static async DownloadAsync(token: string, url: string, path: string): Promise<ApiResponseModel<any>> {
        try {
            const uri = `${url}/${mediaEndpoint}/file/${path}`;
            const response = await HttpClient.getAsync(token, uri)
            if (response.ok) {
                const data = await response.blob();
                if (data === null) {
                    return new ApiResponseModel<any>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<any>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<any>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<any>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }

        } catch (error) {
            return new ApiResponseModel<any>(null, ErrorResponseModel.NewError("MediaApi.DownloadAsync().Exception", error));;
        }
    }
    public static async DeleteAsync(token: string, url: string, path: string): Promise<ApiResponseModel<string>> {
        try {
            const uri = `${url}/${mediaEndpoint}/${path}/delete`;
            const response = await HttpClient.deleteAsync(token, uri)
            if (response.ok) {
                const data = await response.json();
                if (data === null) {
                    return new ApiResponseModel<string>(data, ErrorResponseModel.NewErrorMsg("content-null", "The response body was empty"));
                }
                return new ApiResponseModel<string>(data, null);
            }
            else if (response.status == 400 || response.status == 404) {
                const errorMsg = await response.json();
                const error = response.statusText;
                return new ApiResponseModel<string>(null, ErrorResponseModel.NewErrorMsg(error, errorMsg));
            }
            else if (response.status == 401) {
                const error = response.statusText;
                return new ApiResponseModel<string>(null, ErrorResponseModel.NewErrorMsg(error, "Unauthorized access"));
            }

        } catch (error) {
            return new ApiResponseModel<string>(null, ErrorResponseModel.NewError("MediaApi.DeleteAsync().Exception", error));;
        }
    }
}