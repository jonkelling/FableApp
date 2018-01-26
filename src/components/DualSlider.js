import React from 'react';
import PropTypes from 'prop-types';
import Slider from "react-rangeslider";

export default class DualSlider extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value1: 50,
            value2: 50
        }
        this.onSlider1Change = this.onSlider1Change.bind(this);
        this.onSlider2Change = this.onSlider2Change.bind(this);
    }

    onSlider1Change(value1) {
        this.setState({value1});
        this.props.onSlider1Change &&
        this.props.onSlider1Change(value1);
    }

    onSlider2Change(value2) {
        this.setState({value2});
        this.props.onSlider2Change &&
        this.props.onSlider2Change(value2);
    }

    render() {
        return <div>
            <div><Slider
                min={1}
                max={100}
                orientation='horizontal'
                value={this.props.value1 || this.state.value1}
                onChange={this.onSlider1Change} /></div>
            <div><Slider
                min={1}
                max={100}
                orientation='horizontal'
                value={this.props.value2 || this.state.value2}
                onChange={this.onSlider2Change} /></div>
        </div>
    }
}

DualSlider.propTypes = {
    onSlider1Change: PropTypes.func,
    onSlider2Change: PropTypes.func,
    value1: PropTypes.number,
    value2: PropTypes.number
};