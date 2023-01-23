import ApiResponseModel from "../models/ApiResponseModel";
import CharacterPropModel from "../models/CharacterPropModel";
import { InsertCharacterPropRequestModel } from "../models/InsertCharacterPropRequestModel";
import { UpdateCharacterPropRequestModel } from "../models/UpdateCharacterPropRequestModel";
export default class CharacterPropApi {
    static GetAsync(token: string, url: string, characterId: string): Promise<ApiResponseModel<Array<CharacterPropModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<CharacterPropModel>>;
    static InsertAsync(token: string, url: string, request: InsertCharacterPropRequestModel): Promise<ApiResponseModel<void>>;
    static UpdateAsync(token: string, url: string, request: UpdateCharacterPropRequestModel): Promise<ApiResponseModel<void>>;
    static DeleteAsync(token: string, url: string, id: number): Promise<ApiResponseModel<void>>;
}
