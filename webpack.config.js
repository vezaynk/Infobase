const path = require('path');

module.exports = {
    devtool: false,
    mode: "development",
    target: 'web',
    entry: {
        app: ['@babel/polyfill', './Client/app.ts'],
        vendor: './Client/vendor.ts'
    },
    devtool: "source-map",
    output: {
        path: path.join(__dirname, 'wwwroot', 'ts'),
        filename: '[name].ts',
        chunkFilename: 'app.[name].[hash].ts',
        publicPath: '/ts/'
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                test: /app.ts/,
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
            }
        ]
    },
    resolve: {
        modules: [path.resolve(__dirname, 'Client'), 'node_modules'],
        extensions: ['.js', 'jsx', '.tsx', '.ts']
    }
};
