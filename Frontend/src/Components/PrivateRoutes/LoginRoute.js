import { Navigate, Outlet } from "react-router-dom";
import Landing from "../Landing/Login/Login";
import { useEffect, useState } from "react";

function LoginRoute(){

    var email = sessionStorage.getItem('email');
    var token = sessionStorage.getItem('token');

    var [navigationString,setNavigationString] = useState("");

    useEffect(() => {
        if(token !== null){
            check();
        }
    },[])

    function check(){
        if(email.includes("@maverick.in")){
            setNavigationString("/employeeMenu/allCustomers");
        }
        else if(email.includes("@admin.in")){
            setNavigationString("/adminMenu/allCustomers");
        }
        else{
            setNavigationString("/menu/dashboard");
        }
    }

    return (
        token ? <Navigate to={navigationString}/> : <Landing/> 
    );
}

export default LoginRoute;