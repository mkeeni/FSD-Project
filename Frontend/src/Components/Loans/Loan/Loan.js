import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { updateLoanID } from "../../../loanSlice";

function Loan(props){

    var dispatch = useDispatch();
    var navigate = useNavigate();

    function updateLoanId(){
        navigate("/menu/applyLoan");
        dispatch(
            updateLoanID(props.loan.loanID)
        );
    }

    return(
        <div className="whiteOutlineBox3">
            <div className="whiteOutlineBoxMargin">
                <span className="clickRegisterText">Loan Amount: {props.loan.loanAmount}</span>
                <span className="clickRegisterText">Interest: {props.loan.interest}</span>
                <span className="clickRegisterText">Tenure: {props.loan.tenure}</span>
                <div className="smallBox23">
                    <span className="clickRegisterText">Type: {props.loan.loanType}</span>
                    <span className="btn btn-outline-success smallBox9" onClick={updateLoanId}>
                        <span>Apply</span>
                    </span>
                </div>
            </div>
        </div>
    );
}

export default Loan;