import ApiResponseModel from "../models/ApiResponseModel";
import WorldMiscModel from "../models/WorldMiscModel";
import InsertWorldMiscRequestModel from "../models/InsertWorldMiscRequestModel";
import UpdateWorldMiscRequestModel from "../models/UpdateWorldMiscRequestModel";
export default class WorldMiscApi {
    static GetAsync(token: string, url: string): Promise<ApiResponseModel<Array<WorldMiscModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<WorldMiscModel>>;
    static GetByDependIdAsync(token: string, url: string, dependId: number): Promise<ApiResponseModel<WorldMiscModel>>;
    static CreateAsync(token: string, url: string, request: InsertWorldMiscRequestModel): Promise<ApiResponseModel<void>>;
    static UpdateAsync(token: string, url: string, request: UpdateWorldMiscRequestModel): Promise<ApiResponseModel<void>>;
}
