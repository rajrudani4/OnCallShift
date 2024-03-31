export interface LoggedInUser {
    firstName?: string,
    lastName?: string,
    email?: string,
    userName?: string
    imageUrl?: string, s
    createdAt?: Date,
    createdBy?: number,
    lastUpdatedAt?: Date,
    lastUpdatedBy?: number,
    isGoogleUser?: boolean
    exp?: number
    sub: string
    profileStatus?: string
}
