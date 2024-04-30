import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { updateAccountID } from "../../../accountSlice";

function Account(props){

    var dispatch = useDispatch();
    var navigate = useNavigate();

    const email = sessionStorage.getItem('email');

    function updateAccountId(){
        if(email.includes("@maverick.in")){
            navigate("/employeeMenu/viewDetails");
            dispatch(
                updateAccountID(props.account.accountID)
            );
        }
        else{
            navigate("/menu/viewAccount");
            dispatch(
                updateAccountID(props.account.accountID)
            );
        }
    }

    return (
        <div className="whiteOutlineBox8">
            <div className="whiteOutlineBoxMargin">
                <span className="clickRegisterText">Account No: {props.account.accountNumber}</span>
                <div className="smallBox23">
                    <span className="clickRegisterText">Account type: {props.account.accountType}</span>
                    <span className="pointer" onClick={updateAccountId}>
                        <div className="rightArrow2 change-my-color"></div>
                    </span>
                </div>
                <span className="clickRegisterText">Balance: {props.account.balance}</span>
            </div>
        </div>
    );
}

export default Account;