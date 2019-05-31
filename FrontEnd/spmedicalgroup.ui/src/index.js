import React from 'react';
import ReactDOM from 'react-dom';
import * as serviceWorker from './serviceWorker';
import {Route, BrowserRouter as Router, Switch, Redirect} from "react-router-dom";
import PacienteConsultas from "./pages/consultas/PacienteConsultas";
import MedicoConsultas from "./pages/consultas/MedicoConsultas";
import Login from "./pages/login/Login";
import App from "./pages/home/App";
import ListagemConsultas from "./pages/Listas/ListagemConsultas"
import ListagemUsuarios from "./pages/Listas/ListagemUsuarios"
import {UsuarioAutenticado} from "./services/auth";
import {parseJwt} from "./services/auth";
import CadastroPaciente from './pages/cadastros/CadastroPaciente';
import CadastroConsulta from './pages/cadastros/CadastroConsulta';

//VAI VERIFICAR SE E ADMINISTRADOR
const PermissaoAdmin = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().TipoUsuario == "ADMINISTRADOR" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/"}}/>
            )
        }
    />
)

//ELE VAI VERIFICAR SE E MEDICO
const PermissaoMedico = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().TipoUsuario == "MEDICO" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/"}}/>
            )
        }
    />
)

// ELE VAI VERIFICAR SE É PACIENTE
const PermissaoPaciente = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().TipoUsuario == "PACIENTE" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/"}}/>
            )
        }
    />
)


const Routing = (
    <Router>
        <div>
            <Switch>
                <Route exact path="/" component={Login}/>
                <PermissaoPaciente path="/PacienteConsultas" component={PacienteConsultas} />
                <PermissaoMedico path="/MedicoConsultas" component={MedicoConsultas} />
                <Route path="/CadastroPaciente" component={CadastroPaciente} />
                <Route path="/CadastroConsulta" component={CadastroConsulta} />
                <Route path="/Home" component={App} />
                <Route path="/ListagemConsultas" component={ListagemConsultas}/>
                <Route path="/ListagemUsuarios" component={ListagemUsuarios}/>
                {/* <Route path="/DashBoard" component={DashBoard} />
                 */}
            </Switch>
        </div>
    </Router>
)
ReactDOM.render(Routing, document.getElementById('root'));

serviceWorker.unregister();
