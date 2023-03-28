class Global
{
    private static realm_: string = (
        () => {
            let realm = window.location.protocol + "//" + window.location.hostname;
            if (window.location.port.length !== 0)
            {
                realm += ":" + window.location.port;
            }
            return realm;
        }
    )();

    static get realm(): string {
        return this.realm_;
    }
}

export {Global};
