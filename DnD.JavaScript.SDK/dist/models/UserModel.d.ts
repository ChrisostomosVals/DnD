export default interface UserModel {
    id: string;
    roleId: number;
    role: string;
    characterId: string | null;
    name: string | null;
    email: string;
}
