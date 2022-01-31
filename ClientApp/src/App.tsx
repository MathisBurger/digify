import {BrowserRouter, Route, Switch} from "react-router-dom";
import LoginPage from "./pages/LoginPage";


const App = () => {
  
  return (
      <BrowserRouter>
          <Switch>
              <Route path="/login" component={LoginPage} />
          </Switch>
      </BrowserRouter>
  )
}

export default App;
