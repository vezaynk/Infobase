const path = require('path');

module.exports = {
    devtool: false,
    mode: "development",
    target: 'web',
    entry: {
        app: './Client/App.tsx',
        vendor: './Client/Vendor.tsx'
    },
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
                loader: "ts-loader",
                exclude: /node_modules/,
                options: {
                    transpileOnly: true
                }
            },
            {
                test: /App.tsx/,
                loader: "expose-loader?Components"
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
            },
        ]
    },
    resolve: {
        modules: [path.resolve(__dirname, 'Client'), 'node_modules'],
        extensions: ['.ts', '.tsx', '.js']
    }
};
