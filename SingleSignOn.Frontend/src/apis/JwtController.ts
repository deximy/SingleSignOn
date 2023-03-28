import {Global} from "./Global";

const GetJwtBySteamToken = async (steam_token: string) => {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");

    let response = await fetch(
        `${Global.realm}/jwts`,
        {
            method: "POST",
            headers: headers,
            body: JSON.stringify(steam_token),
        }
    );
    console.log(response);
};

export {GetJwtBySteamToken};
