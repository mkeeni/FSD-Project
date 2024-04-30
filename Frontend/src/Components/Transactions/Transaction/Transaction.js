import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { updatetransactionID } from "../../../transactionSlice";

function Transaction(props){

    var dispatch = useDispatch();
    var navigate = useNavigate();

    const email = sessionStorage.getItem('email');

    function updatetransactionId(){
        if(email.includes("@maverick.in")){
            navigate("/employeeMenu/viewTransaction");
            dispatch(
                updatetransactionID(props.transaction.transactionID)
            );
        }
        else{
            navigate("/menu/viewTransaction");
            dispatch(
                updatetransactionID(props.transaction.transactionID)
            );
        }
    }

    return (
        <div className="whiteOutlineBox2">
            <div className="whiteOutlineBoxMargin">
                    <div className="smallBox23">
                        {props.transaction.status === "Success" ? <span className="clickRegisterText">{props.transaction.transactionType}</span> :
                        <span className="clickRegisterText">{props.transaction.transactionType} Failed</span>
                        }
                        <div className="transactiondetails">
                            <span className="clickRegisterText">{props.transaction.amount}</span>
                            <span className="pointer" onClick={updatetransactionId}>
                                {props.transaction.status === "Success" ? <div className="rightArrow change-my-color"></div> :
                                <div className="rightArrow change-my-color2"></div>
                                }
                            </span>
                        </div>
                    </div>
            </div>
        </div>
    );
}

export default Transaction;