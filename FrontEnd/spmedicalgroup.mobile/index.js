/**
 * @format
 */

import {AppRegistry} from 'react-native';
import App from './App';
import {name as appName} from './app.json';
import Navigator from './src/index';

AppRegistry.registerComponent(appName, () => Navigator);
