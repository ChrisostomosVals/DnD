import ApiResponseModel from "../models/ApiResponseModel";
export declare class MediaApi {
    static UploadAsync(token: string, url: string, type: string, name: string, files: File[]): Promise<ApiResponseModel<string>>;
    static DownloadAsync(token: string, url: string, path: string): Promise<ApiResponseModel<any>>;
    static DeleteAsync(token: string, url: string, path: string): Promise<ApiResponseModel<string>>;
}
