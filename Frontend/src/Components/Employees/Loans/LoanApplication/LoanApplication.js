import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { updateLoanID } from "../../../../loanSlice";

function LoanApplication(props){

    var dispatch = useDispatch();
    var navigate = useNavigate();

    function updateLoanId(){
        navigate("/employeeMenu/viewLoan");
        dispatch(
            updateLoanID(props.loan.loanApplicationID)
        );
    }

    return (
        <div className="whiteOutlineBox8">
            <div className="whiteOutlineBoxMargin">
                <span className="clickRegisterText">Loan Amount: {props.loan.amount}</span>
                <div className="smallBox23">
                    <span className="clickRegisterText">Purpose: {props.loan.purpose}</span>
                    <span className="pointer" onClick={updateLoanId}>
                        <div className="rightArrow2 change-my-color"></div>
                    </span>
                </div>
                <span className="clickRegisterText">Applicant Name: {props.loan.customers.name}</span>
            </div>
        </div>
    );  
}

export default LoanApplication;