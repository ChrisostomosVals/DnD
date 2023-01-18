import ApiResponseModel from "../models/ApiResponseModel";
import CharacterArsenalModel from "../models/CharacterArsenalModel";
import InsertCharacterArsenalRequestModel from "../models/InsertArsenalRequestModel";
import UpdateCharacterArsenalRequestModel from "../models/UpdateCharacterArsenalRequestModel";
export default class CharacterArsenalApi {
    static GetAsync(token: string, url: string, characterId: string): Promise<ApiResponseModel<Array<CharacterArsenalModel>>>;
    static GetByIdAsync(token: string, url: string, id: number): Promise<ApiResponseModel<CharacterArsenalModel>>;
    static InsertItemAsync(token: string, url: string, request: InsertCharacterArsenalRequestModel): Promise<ApiResponseModel<void>>;
    static UpdateItemAsync(token: string, url: string, request: UpdateCharacterArsenalRequestModel): Promise<ApiResponseModel<void>>;
    static DeleteItemAsync(token: string, url: string, id: number): Promise<ApiResponseModel<void>>;
}
