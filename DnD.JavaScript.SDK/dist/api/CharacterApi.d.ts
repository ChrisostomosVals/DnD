import ApiResponseModel from "../models/ApiResponseModel";
import CharacterModel from "../models/CharacterModel";
import CreateCharacterRequestModel from "../models/CreateCharacterRequestModel";
import UpdateCharacterRequestModel from "../models/UpdateCharacterRequestModel";
export default class CharacterApi {
    static GetAsync(token: string, url: string): Promise<ApiResponseModel<Array<CharacterModel>>>;
    static GetByIdAsync(token: string, url: string, id: string): Promise<ApiResponseModel<CharacterModel>>;
    static CreateAsync(token: string, url: string, request: CreateCharacterRequestModel): Promise<ApiResponseModel<void>>;
    static UpdateAsync(token: string, url: string, request: UpdateCharacterRequestModel): Promise<ApiResponseModel<void>>;
    static DeleteAsync(token: string, url: string, id: string): Promise<ApiResponseModel<void>>;
}
