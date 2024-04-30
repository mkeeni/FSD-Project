import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { updateAccountID } from "../../../../accountSlice";


function Request(props){

    var dispatch = useDispatch();
    var navigate = useNavigate();

    function updateAccountId(){
        navigate("/employeeMenu/viewOpenRequest");
        dispatch(
            updateAccountID(props.account.accountID)
        );
    }

    return(
        <div className="whiteOutlineBox5">
            <div className="whiteOutlineBoxMargin">
            <span className="clickRegisterText">Account type: {props.account.accountType}</span>
                <div className="smallBox23">
                    <span className="clickRegisterText">Account No: {props.account.accountNumber}</span>
                    <span className="pointer" onClick={updateAccountId}>
                        <div className="rightArrow2 change-my-color"></div>
                    </span>
                </div>
            </div>
        </div>
    );
}

export default Request;