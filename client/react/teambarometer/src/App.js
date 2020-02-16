import React, { Component } from 'react';
// import logo from './logo.svg';
import playImage from './play-button.png';
import './App.css';

// function App() {
//   return (
//     <div className="App">
//       <header className="App-header">
//         <img src={logo} className="App-logo" alt="logo" />
//         <p>
//           Edit <code>src/App.js</code> and save to reload.
//         </p>
//         <a
//           className="App-link"
//           href="https://reactjs.org"
//           target="_blank"
//           rel="noopener noreferrer"
//         >
//           Learn React
//         </a>
//       </header>
//     </div>
//   );
// }

// export default App;

export default class App extends Component {
  render() {
    return (
      <main className="container">
        <div className="row">
            <header className="col-sm">
                <h1>Team Barometer</h1>
            </header>
        </div>
        <div className="row">
            <div className="questions col-sm-6">
                <ul>
                    <li className="current-question">
                        <div className="question d-flex">
                            <div className="mr-auto">Confiança</div>
                            <div className="cont-red">4</div>
                            <div className="cont-yellow">2</div>
                            <div className="cont-green">4</div>
                            <div className="play">
                                <img src={playImage} alt=""></img>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Colaboração no Time</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Feedback</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Reuniões Participativas</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Compromisso</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Melhora Contínua</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Responsabilidade Mútua</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Autonomia</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Orgulho</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Relações</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Protagonismo</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Compartilhar</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Nos Capacitar</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Lealdade</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Motivação</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Integridade</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Ambiente de Trabalho</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Colaboração entre Times</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Inteligência Emocional</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Objetivos</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Aprendizagem</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Diante das Falhas</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Compromisso com a Qualidade</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Propósito</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Disciplinado</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Resiliência</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                    <li>
                        <div className="question d-flex">
                            <div className="mr-auto">Continuidade</div>
                            <div className="cont-red"></div>
                            <div className="cont-yellow"></div>
                            <div className="cont-green"></div>
                        </div>
                    </li>
                </ul>
            </div>
            <div className="answers col-sm">
                <div className="red">
                    Nós raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.
                </div>
                <div className="yellow"></div>
                <div className="green">
                    Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões
                    e
                    conflitos construtivo.
                </div>
            </div>
        </div>
    </main>
    );
  }
}