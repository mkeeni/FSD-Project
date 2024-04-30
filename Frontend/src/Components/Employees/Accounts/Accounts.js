import { useState } from "react";
import '../../style.css';
import { Outlet, useNavigate } from "react-router-dom";


function Accounts() {
    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <Outlet/>
            </div>
        </div>
    );
}

export default Accounts;