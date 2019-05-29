const path = require('path');
const webpack = require('webpack');

module.exports = [{
    devtool: false,
    mode: "development",
    target: 'web',
    entry: {
        server: './Client/server.ts'
    },
    devtool: "source-map",
    output: {
        path: path.join(__dirname, 'wwwroot', 'js'),
        filename: '[name].js',
        chunkFilename: 'app.[name].[hash].js',
        publicPath: '/js/'
    },
    node: {
        tls: "empty",
        fs: "empty",
        net: "empty",
        child_process: "empty"
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
        ]
    },
    resolve: {
        modules: [path.resolve(__dirname, 'Client'), 'node_modules'],
        extensions: ['.js', 'jsx', '.tsx', '.ts']
    },
    plugins: [new webpack.ProvidePlugin({
        window: [path.resolve(path.join(__dirname, 'Client/common/shim.js')), 'stub'],
        setTimeout: [path.resolve(path.join(__dirname, 'Client/common/shim.js')), 'callback'],
        clearTimeout: [path.resolve(path.join(__dirname, 'Client/common/shim.js')), 'noCallback']
    })]
}, {
    devtool: false,
    mode: "development",
    target: 'web',
    entry: {
        client: './Client/client.ts'
    },
    devtool: "source-map",
    output: {
        path: path.join(__dirname, 'wwwroot', 'js'),
        filename: '[name].js',
        chunkFilename: 'app.[name].[hash].js',
        publicPath: '/js/'
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                test: require.resolve('react-dom'),
                use: [{
                    loader: 'expose-loader',
                    options: 'ReactDOM'
                }]
            },
            {
                test: require.resolve('react'),
                use: [{
                    loader: 'expose-loader',
                    options: 'React'
                }]
            }
        ]
    },
    resolve: {
        modules: [path.resolve(__dirname, 'Client'), 'node_modules'],
        extensions: ['.js', 'jsx', '.tsx', '.ts']
    }
}];
